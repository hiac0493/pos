using CheckPoint.WebApi.Metadata;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Pos.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckPoint.WebApi.Controllers.POS
{
    public class ProveedoresController : BaseController
    {
        public ProveedoresController(IOptions<DatabaseSettings> dbSettings, IHostingEnvironment environment) : base(dbSettings, environment)
        {
            DbSettings = dbSettings;
            Environment = environment;
        }

        [HttpPost]
        [Route("SaveProveedor")]
        public IActionResult SaveProveedor([FromBody] Proveedores proveedor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (proveedor.idProveedor == 0)
                    {
                        PosUoW.ProveedoresRepository.Add(proveedor);
                    }
                    else
                    {
                        PosUoW.ProveedoresRepository.Update(proveedor);
                    }
                    PosUoW.Save();
                    return Ok(PosUoW.ProveedoresRepository.GetSupplierWithProductsById(proveedor.idProveedor));
                }
                else
                {
                    return BadRequest("Los datos del proveedor son incorrectos");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("GetAllProveedores")]
        public IActionResult GetAllProveedores()
        {
            try
            {
                List<Proveedores> proveedoresList = PosUoW.ProveedoresRepository.GetAllByCriteria(a => a.Estatus, a => a.idProveedor).ToList();
                if(proveedoresList != null)
                {
                    return Ok(proveedoresList);
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
        [Route("GetSuppliersWithProducts")]  
        public IActionResult GetSuppliersWithProducts()
        {
            try
            {
                var proveedoresList = PosUoW.ProveedoresRepository.GetSuppliersWithProducts().ToList();

                if(proveedoresList != null)
                {
                    return Ok(proveedoresList); 
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
        [Route("GetSuppliersByName")]
        public IActionResult GetSuppliersByName(string name)
        {
            try
            {
                IEnumerable<Proveedores> suppliersResultList = PosUoW.ProveedoresRepository.GetSuppliersByName(name);
                if(suppliersResultList != null)
                {
                    return Ok(suppliersResultList);
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
        [Route("GetProveedorById")]
        public IActionResult GetProveedorById(int idProveedor)
        {
            try
            {
                Proveedores proveedor = PosUoW.ProveedoresRepository.GetById(a => a.idProveedor.Equals(idProveedor));
                if (proveedor != null)
                {
                    return Ok(proveedor);
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
        [Route("DeleteProductoProveedor")]
        public IActionResult DeleteProductoProveedor([FromBody] ProductosProveedor productoProveedor)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ProductosProveedor productosProveedorResult = PosUoW.ProductosProveedorRepository.GetSingleByCriteria(x => x.idProducto.Equals(productoProveedor.idProducto) && x.idProveedor.Equals(productoProveedor.idProveedor));
                    if(productosProveedorResult != null)
                        PosUoW.ProductosProveedorRepository.Remove(productosProveedorResult);
                    PosUoW.Save();
                    return Ok();
                }
                else
                {
                    return BadRequest("Los datos del proveedor no son correctos");
                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
