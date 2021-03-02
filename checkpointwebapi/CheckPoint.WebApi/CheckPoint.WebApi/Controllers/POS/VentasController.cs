using CheckPoint.WebApi.Dtos;
using CheckPoint.WebApi.Metadata;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pos.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckPoint.WebApi.Controllers.POS
{
    public class VentasController : BaseController
    {
        public VentasController(IOptions<DatabaseSettings> dbSettings, IHostingEnvironment environment) : base(dbSettings, environment)
        {
            DbSettings = dbSettings;
            Environment = environment;
        }

        [HttpGet]
        [Route("GetAllVentas")]
        public IActionResult GetAllVentas()
        {
            try
            {
                List<Ventas> ventasList = PosUoW.VentasRepository.GetAll(a => a.FolioVenta).ToList();
                if (ventasList != null)
                {
                    return Ok(ventasList);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("GetAllPagosCorte")]
        public IActionResult GetAllPagosCorte(int folioInicio, int folioFinal, int IdUsuario)
        {
            try
            {
                var pagosResult = PosUoW.VentasRepository.GetAllPagosCorte(folioInicio, folioFinal, IdUsuario);
                if (pagosResult != null)
                {
                    return Ok(pagosResult);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("GetVentaByFolioVenta")]
        public IActionResult GetVentaByFolioVenta(int folioVenta)
        {
            try
            {
                Ventas venta = PosUoW.VentasRepository.GetVentaByFolio(folioVenta);
                if(venta != null)
                {
                    return Ok(venta);
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("AddVenta")]
        public IActionResult AddVenta([FromBody] Ventas venta)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    float utilidad = 0;
                    foreach (ProductosVenta item in venta.Productos)
                    {
                        Productos producto = PosUoW.ProductosRepository.GetById(a => a.idProducto.Equals(item.idProducto));
                        List<ProductosCompra> productosCompra = PosUoW.ProductosCompraRepository.GetAllByCriteria(x => x.idProducto.Equals(producto.idProducto) && x.Restante > 0, x => x.idCompraProducto).ToList();
                        float restante = item.Cantidad;
                        for (int i = 0; i < productosCompra.Count(); i++)
                        {
                            VentaLote lote = new VentaLote();
                            ProductosCompra productoCompra = productosCompra.ElementAt(i);
                            if (productoCompra.Restante == restante)
                            {
                                lote.idProducto = producto.idProducto;
                                lote.idProductoCompra = productoCompra.idCompraProducto;
                                lote.estatus = true;
                                lote.cantidad = restante;
                                restante = 0;
                                utilidad += productoCompra.Restante * productoCompra.Costo;
                                productoCompra.Restante = 0;
                                i = productosCompra.Count();
                            }
                            else if (productoCompra.Restante > restante)
                            {
                                lote.idProducto = producto.idProducto;
                                lote.idProductoCompra = productoCompra.idCompraProducto;
                                lote.estatus = true;
                                lote.cantidad = restante;
                                utilidad += restante * productoCompra.Costo;
                                productoCompra.Restante -= restante;
                                i = productosCompra.Count();
                                restante = 0;
                            }
                            else
                            {
                                lote.idProducto = producto.idProducto;
                                lote.idProductoCompra = productoCompra.idCompraProducto;
                                lote.estatus = true;
                                lote.cantidad = productoCompra.Restante;
                                utilidad += productoCompra.Restante * productoCompra.Costo;
                                restante -= productoCompra.Restante;
                                productoCompra.Restante = 0;
                            }
                            venta.Lotes.Add(lote);
                            PosUoW.ProductosCompraRepository.Update(productoCompra);
                        }
                        if (restante > 0)
                            utilidad += restante * producto.PrecioCosto;
                        producto.Existencia -= item.Cantidad;
                        PosUoW.ProductosRepository.Update(producto);
                    }
                    utilidad = (float)Math.Round((venta.Total - venta.Impuestos) - utilidad, 2);
                    venta.Utilidad = utilidad;
                    PosUoW.VentasRepository.Add(venta);
                    PosUoW.Save();
                    return Ok(venta);
                }
                else
                {
                    return BadRequest("Los datos de la venta son incorrectos");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("CancelaVenta")]
        public IActionResult CancelaVenta([FromBody] CancelacionDto cancelacion)
        {
            try
            {
                Ventas venta = PosUoW.VentasRepository.GetVentaByFolio(cancelacion.folioVenta);
                if (venta != null)
                {
                    venta.Estatus = 'C';
                    venta.idUsuarioCancela = cancelacion.idUsuario;
                    foreach (VentaLote lote in venta.Lotes)
                    {
                        ProductosCompra compra = PosUoW.ProductosCompraRepository.GetById(a => a.idCompraProducto.Equals(lote.idProductoCompra));
                        if (compra != null)
                            compra.Restante += lote.cantidad;
                        lote.estatus = false;
                    }
                    foreach (ProductosVenta producto in venta.Productos)
                    {
                        Productos currentProduct = PosUoW.ProductosRepository.GetById(x => x.idProducto.Equals(producto.idProducto));
                        currentProduct.Existencia += producto.Cantidad;
                    }
                    PosUoW.Save();
                    return Ok(venta);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("GetVentaByFolio")]
        public IActionResult GetVentaByFolio(long folioVenta)
        {
            try
            {
                Ventas venta = PosUoW.VentasRepository.GetById(a => a.FolioVenta.Equals(folioVenta));
                if (venta != null)
                {
                    return Ok(venta);
                }
                else
                {
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        [HttpGet]
        [Route("GetTotalSale")]
        public IActionResult GetTotalSale(int idUsuario, long folioInicio, long folioFin)
        {
            try
            {
                var sale = PosUoW.VentasRepository.GetTotalSale(idUsuario, folioInicio, folioFin);
                if (sale != 0)
                {
                    return Ok(sale);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("GetLastFolio")]
        public IActionResult GetLastFolio()
        {
            try
            {
                var sale = PosUoW.VentasRepository.GetLastFolio();
                if (sale != 0)
                {
                    return Ok(sale);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("GetTotalWithTaxes")]
        public IActionResult GetTotalWithTaxes(long folioInicio, long folioFinal, int IdUsuario)
        {
            try
            {
                var sale = PosUoW.VentasRepository.GetTotalWithTaxes(folioInicio, folioFinal, IdUsuario);
                if (sale != null)
                {
                    return Ok(sale);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("GetReturnsWithTaxes")]
        public IActionResult GetReturnsWithTaxes(int folioInicio, int folioFinal, int IdUsuario)
        {
            try
            {
                var sale  = PosUoW.VentasRepository.GetReturnsWithTaxes(folioInicio, folioFinal, IdUsuario);
                if (sale != null)
                {
                    return Ok(sale);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("CalcIvaTasa")]
        public IActionResult CalcIvaTasa(int IdUsuario, long folioInicio, long folioFinal)
        {
            try
            {
                double sale = PosUoW.VentasRepository.CalcIvaTasa(IdUsuario, folioInicio, folioFinal);
                if (sale != 0)
                {
                    return Ok(sale);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        [HttpGet]
        [Route("GetTotalReturns")]
        public IActionResult GetTotalReturns(int folioInicio, int folioFinal, int IdUsuario)
        {
            try
            {
                double sale = PosUoW.VentasRepository.GetTotalReturn(IdUsuario, folioInicio, folioFinal);
                if (sale != 0)
                {
                    return Ok(sale);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }



        [HttpGet]
        [Route("CalcIvaReturn")]
        public IActionResult CalcIvaReturn(int IdUsuario, int folioInicio, int folioFinal)
        {
            try
            {
                double sale = PosUoW.VentasRepository.CalcIvaReturn(IdUsuario, folioInicio, folioFinal);
                if (sale != 0)
                {
                    return Ok(sale);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        

    }
}
