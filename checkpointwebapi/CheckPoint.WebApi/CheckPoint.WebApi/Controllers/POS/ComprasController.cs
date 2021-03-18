using CheckPoint.WebApi.Metadata;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pos.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckPoint.WebApi.Controllers.POS
{
    public class ComprasController : BaseController
    {
        public ComprasController(IOptions<DatabaseSettings> dbSettings, IHostingEnvironment environment) : base(dbSettings, environment)
        {
            DbSettings = dbSettings;
            Environment = environment;
        }

        [HttpPost]
        [Route("AddCompra")]
        public IActionResult AddCompra(long orden, [FromBody] Compras compra)
        {
            try
             {
                if (ModelState.IsValid)
                {
                    foreach (ProductosCompra item in compra.ProductosCompra)
                    {

                        ProductoAlmacen productoAlmacen = PosUoW.ProductoAlmacenRepository.GetSingleByCriteria(x => x.idProducto.Equals(item.idProducto) && x.idAlmacen.Equals(compra.idAlmacen));
                        List<ProductoAlmacen> existenciaProducto = PosUoW.ProductoAlmacenRepository.GetAllByCriteria(x => x.idProducto.Equals(item.idProducto), x => x.idProductoAlmacen).ToList();
                        float existencia = existenciaProducto.Sum(x => x.Existencia);
                        Productos producto = PosUoW.ProductosRepository.GetById(a => a.idProducto.Equals(item.idProducto));
                        float dif = existencia < 0 ? existencia : 0;
                        item.Restante = item.Cantidad + dif;
                        item.Costo = item.Monto / item.Cantidad;
                        if (productoAlmacen != null)
                        {
                            productoAlmacen.Existencia += item.Cantidad;
                            PosUoW.ProductoAlmacenRepository.Update(productoAlmacen);
                        }
                        else
                        {
                            productoAlmacen = new ProductoAlmacen
                            {
                                Existencia = item.Cantidad,
                                idAlmacen = compra.idAlmacen,
                                idProducto = producto.idProducto
                            };
                            PosUoW.ProductoAlmacenRepository.Add(productoAlmacen);
                        }

                        if (item.Costo != producto.PrecioCosto)
                        {
                            producto.PrecioCosto = item.Costo;
                            PosUoW.ProductosRepository.Update(producto);
                        }    
                    }
                    Ordenes ordenResult = PosUoW.OrdenesRepository.GetById(x => x.idOrden.Equals(orden));
                    PosUoW.ComprasRepository.Add(compra);
                    PosUoW.Save();
                    if(ordenResult != null)
                    {
                        ordenResult.idCompra = compra.FolioCompra;
                        ordenResult.Estatus = 'S';
                        PosUoW.OrdenesRepository.Update(ordenResult);
                    }
                    PosUoW.Save();
                    return Ok(compra);
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
    }
}
