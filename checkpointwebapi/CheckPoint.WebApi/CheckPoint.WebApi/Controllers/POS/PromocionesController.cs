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
    public class PromocionesController : BaseController
    {
        public PromocionesController(IOptions<DatabaseSettings> dbSettings, IHostingEnvironment environment) : base(dbSettings, environment)
        {
            DbSettings = dbSettings;
            Environment = environment;
        }

        [HttpGet]
        [Route("GetAllPromociones")]
        public IActionResult GetAllPromociones()
        {
            try
            {
                List<Promociones> promocionesList = PosUoW.PromocionesRepository.GetAllByCriteria(x => x.Estatus, x => x.idPromocion).ToList();
                if (promocionesList != null)
                    return Ok(promocionesList);
                else
                    return NotFound();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("GetPromocionById")]
        public IActionResult GetPromocionById(long idPromocion)
        {
            try
            {
                var promocion = PosUoW.PromocionesRepository.GetPromocionById(idPromocion);
                if (promocion != null)
                    return Ok(promocion);
                else
                    return NotFound();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("SavePromocion")]
        public IActionResult SaveCashClose([FromBody] Promociones promocion)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (promocion.idPromocion == 0)
                    {
                        PosUoW.PromocionesRepository.Add(promocion);
                    }
                    else
                    {
                        PosUoW.PromocionesRepository.Update(promocion);
                    }
                    PosUoW.Save();
                    return Ok(promocion);
                }
                else
                {
                    return BadRequest("Los datos de la marca son incorrectos");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete]
        [Route("DeleteProductPromotion")]
        public IActionResult DeleteProductPromotion(long idProductoPromocion)
        {
            try
            {
                ProductosPromocion producto = PosUoW.ProductosPromocionRepository.GetById(x => x.idProductoPromocion.Equals(idProductoPromocion));
                if (producto != null)
                {
                    PosUoW.ProductosPromocionRepository.Remove(producto);
                    PosUoW.Save();
                    return Ok();
                }                    
                else
                    return NotFound();
                
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
