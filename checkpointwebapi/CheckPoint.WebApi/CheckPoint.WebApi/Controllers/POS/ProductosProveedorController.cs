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
    public class ProductosProveedorController : BaseController
    {
        public ProductosProveedorController(IOptions<DatabaseSettings> dbSettings, IHostingEnvironment environment) : base(dbSettings, environment)
        {
            DbSettings = dbSettings;
            Environment = environment;
        }

        [HttpDelete]
        [Route("DeleteProductoProveedorById")]
        public IActionResult DeleteProductoProveedorById(int idProductoProveedor)
        {
            ProductosProveedor proveedor = PosUoW.ProductosProveedorRepository.GetById(x => x.idProductoProveedor.Equals(idProductoProveedor));
            try
            {
                if (proveedor != null)
                {
                    PosUoW.ProductosProveedorRepository.Remove(proveedor);
                    PosUoW.Save();
                    return Ok();
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
    }
}
