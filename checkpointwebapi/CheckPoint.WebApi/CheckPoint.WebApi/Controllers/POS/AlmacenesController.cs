using checkpoint.Views.Inventory.Models;
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
    public class AlmacenesController : BaseController
    {
        public AlmacenesController(IOptions<DatabaseSettings> dbSettings, IHostingEnvironment environment) : base(dbSettings, environment)
        {
            DbSettings = dbSettings;
            Environment = environment;
        }

        [HttpGet]
        [Route("GetAllAlmacenes")]
        public IActionResult GetAllAlmacenes()
        {
            try
            {
                List<Almacenes> almacenesList = PosUoW.AlmacenesRepository.GetAllByCriteria(x => x.Activo, x => x.idAlmacen).ToList();
                if (almacenesList != null)
                {
                    return Ok(almacenesList);
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
        [Route("GetProductosByAlmacen")]
        public IActionResult GetProductosByAlmacen(int idAlmacen)
        {
            try
            {
                var productosAlmacen = PosUoW.AlmacenesRepository.GetProductosByAlmacen(idAlmacen).ToList();
                if (productosAlmacen != null)
                {
                    return Ok(productosAlmacen);
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
        [Route("SaveAlmacenes")]
        public IActionResult SaveAlmacenes([FromBody] Almacenes almacen)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (almacen.idAlmacen == 0)
                    {
                        PosUoW.AlmacenesRepository.Add(almacen);
                    }
                    else
                    {
                        PosUoW.AlmacenesRepository.Update(almacen);
                    }
                    PosUoW.Save();
                    return Ok(almacen);
                }
                else
                {
                    return BadRequest("Los datos del almacen son incorrectos");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
