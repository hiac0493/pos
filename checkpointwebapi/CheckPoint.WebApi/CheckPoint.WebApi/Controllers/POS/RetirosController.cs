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
    public class RetirosController : BaseController
    {
        public RetirosController(IOptions<DatabaseSettings> dbSettings, IHostingEnvironment environment) : base(dbSettings, environment)
        {
            DbSettings = dbSettings;
            Environment = environment;
        }

        [HttpPost]
        [Route("SaveRetiro")]
        public IActionResult SaveRetiro([FromBody] Retiro retiro)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (retiro.IdRetiro == 0)
                    {
                        PosUoW.RetirosRepository.Add(retiro);
                    }
                    else
                    {
                        PosUoW.RetirosRepository.Update(retiro);
                    }
                    PosUoW.Save();
                    return Ok(retiro);
                }
                else
                {
                    return BadRequest("Error al guardar el retiro");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("GetAllRetiros")]
        public IActionResult GetAllRetiros(int idcorte)
        {
            try
            {
                List<Retiro> result = PosUoW.RetirosRepository.GetAllByCriteria(x => x.IdCorte.Equals(idcorte), x => x.IdRetiro).ToList();
                if (result != null)
                {
                    return Ok(result);
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
