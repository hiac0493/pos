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
                        Productos producto = PosUoW.ProductosRepository.GetById(a => a.idProducto.Equals(item.idProducto));
                        float dif = producto.Existencia < 0 ? producto.Existencia : 0;
                        producto.Existencia += item.Cantidad;
                        item.Restante = item.Cantidad + dif;
                        item.Costo = item.Monto / item.Cantidad;
                        if(item.Costo != producto.PrecioCosto)
                        {
                            producto.PrecioCosto = item.Costo;
                            PosUoW.ProductosRepository.Update(producto);
                        }    
                        PosUoW.ProductosRepository.Update(producto);
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
