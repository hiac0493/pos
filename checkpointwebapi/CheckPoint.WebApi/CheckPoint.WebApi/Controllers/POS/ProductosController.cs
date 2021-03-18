using CheckPoint.WebApi.Dtos;
using CheckPoint.WebApi.Metadata;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.Extensions.Options;
using Pos.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckPoint.WebApi.Controllers.POS
{
    public class ProductosController : BaseController
    {
        public ProductosController(IOptions<DatabaseSettings> dbSettings, IHostingEnvironment environment) : base(dbSettings, environment)
        {
            DbSettings = dbSettings;
            Environment = environment;
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public IActionResult GetAllProductos()
        {
            try
            {
                List<Productos> productosList = PosUoW.ProductosRepository.GetAllProducts().ToList();
                if (productosList != null)
                {
                    return Ok(productosList);
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
        [Route("GetAllProductsByDepartment")]
        public IActionResult GetAllProductsByDepartment(int idDepartamento)
        {
            try
            {
                var productsList = idDepartamento != 0 ? PosUoW.ProductosRepository.GetAllProductsByDepartment(idDepartamento) : PosUoW.ProductosRepository.GetAllProductsByDepartment();
                if(productsList != null)
                {
                    return Ok(productsList);
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
        [Route("GetProductosByName")]
        public IActionResult GetProductosByName(string nombre)
        {
            try
            {
                var productosList = PosUoW.ProductosRepository.GetProductosByCriteria(nombre).ToList();
                if (productosList != null)
                {
                    return Ok(productosList);
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
        [Route("GetProductoWithSuppliers")]
        public IActionResult GetProductoWithSuppliers(string pluProducto)
        {
            try
            {
                PLUProductos producto = PosUoW.PLUProductoRepository.GetSingleByCriteria(a => a.PLU.Equals(pluProducto));
                var result = producto != null ? PosUoW.ProductosRepository.GetProductosWithSuppliers(producto.idProducto) : null;
                if (result != null)
                {
                    return Ok(result);
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
        [Route("GetProductoByPLU")]
        public IActionResult GetProductoByPLU(string PLU)
        {
            try
            {
                var producto = PosUoW.PLUProductoRepository.GetProductoVenta(PLU);

                if (producto != null)
                {
                    return Ok(producto);
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
        [Route("GetProductObjectByPLU")]
        public IActionResult GetProductObjectByPLU(string plu)
        {
            try
            {
                Productos productResult = PosUoW.ProductosRepository.GetProductObjectByPLU(plu);
                if(productResult != null)
                {
                    return Ok(productResult);
                }
                else
                {
                    return NotFound();
                }
            }catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("GetAllPLUS")]
        public IActionResult GetAllPLUS()
        {
            try
            {
                List<PLUProductos> plusList = PosUoW.PLUProductoRepository.GetAll(a => a.idPLU).ToList();
                if (plusList != null)
                {
                    return Ok(plusList);
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
        [Route("GetProductoById")]
        public IActionResult GetProductoById(int idProducto)
        {
            try
            {
                Productos producto = PosUoW.ProductosRepository.GetById(a => a.idProducto.Equals(idProducto));
                if (producto != null)
                {
                    return Ok(producto);
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

        [HttpPost]
        [Route("SaveProduct")]
        public IActionResult SaveProduct([FromBody] Productos producto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (producto.idProducto == 0)
                        PosUoW.ProductosRepository.Add(producto);
                    else
                        PosUoW.ProductosRepository.Update(producto);
                    PosUoW.Save();
                    return Ok(producto);
                }
                else
                {
                    return BadRequest("Los datos del producto son incorrectos");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("SaveProducts")]
        public IActionResult SaveProducts([FromBody] List<Productos> producto)
        {
            try
            {
                if (producto != null && producto.Count() > 0)
                {
                    PosUoW.ProductosRepository.AddRange(producto);
                    PosUoW.Save();
                    return Ok(producto);
                }
                else
                {
                    return BadRequest("Los datos del producto son incorrectos");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("GetProductsByList")]
        public IActionResult GetProductsByList([FromBody] List<ProductosOrden> productsList)
        {
            try
            {
                var productsResult = PosUoW.ProductosRepository.GetProductsByList(productsList);
                if(productsResult != null)
                {
                    return Ok(productsResult);
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
        [Route("GetProductsSuggestToBuy")]
        public IActionResult GetProductsSuggestToBuy(int supplier)
        {
            try
            {
                var productsToBuySuggestResult = supplier != 0 ? PosUoW.ProductosRepository.GetProductsSuggestToBuy(supplier) : PosUoW.ProductosRepository.GetProductsSuggestToBuy();
                if(productsToBuySuggestResult != null)
                {
                    return Ok(productsToBuySuggestResult);
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

        [HttpDelete]
        [Route("DeletePluProductoById")]
        public IActionResult DeletePluProductoById(int idPlu)
        {
            PLUProductos plu = PosUoW.PLUProductoRepository.GetById(x => x.idPLU.Equals(idPlu));
            try
            {
                if (plu != null)
                {
                    PosUoW.PLUProductoRepository.Remove(plu);
                    PosUoW.Save();
                    return Ok();
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