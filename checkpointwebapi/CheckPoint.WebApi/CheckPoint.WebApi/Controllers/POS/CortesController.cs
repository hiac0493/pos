using CheckPoint.WebApi.Metadata;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class CortesController : BaseController
    {
        public CortesController(IOptions<DatabaseSettings> dbSettings, IHostingEnvironment environment) : base(dbSettings, environment)
        {
            DbSettings = dbSettings;
            Environment = environment;
        }

        [HttpPost]
        [Route("SaveCashClose")]
        public IActionResult SaveCashClose([FromBody] Cortes corte)
        {
            try
             {
                if (ModelState.IsValid)
                {
                    if (corte.IdCorte == 0)
                    {
                        PosUoW.CortesRepository.Add(corte);
                    }
                    else
                    {
                        PosUoW.CortesRepository.Update(corte);
                    }
                    PosUoW.Save();
                    return Ok(PosUoW.CortesRepository.GetCorteInfo(corte));
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

        [HttpGet]
        [Route("GetCurrentCashClose")]
        public IActionResult GetCurrentCashClose(int idUsuario)
        {
            try
            {
                Cortes corte = PosUoW.CortesRepository.GetCurrentCashClose(idUsuario);
                if (corte != null)
                {
                    return Ok(corte);
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
