using CheckPoint.WebApi.Metadata;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pos.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckPoint.WebApi.Controllers.POS
{
    public class TurnosController : BaseController
    {
        public TurnosController(IOptions<DatabaseSettings> dbSettings, IHostingEnvironment enviroment) : base(dbSettings, enviroment)
        {
            DbSettings = dbSettings;
            Environment = enviroment;
        }

        [HttpGet]
        [Route("GetAllTurnos")]
        public IActionResult GetAllTurnos()
        {
            try
            {
                List<Turnos> turnoList = PosUoW.TurnosRepository.GetAllByCriteria(x => x.Estatus, x => x.IdTurno).ToList();
                if (turnoList != null)
                {
                    return Ok(turnoList);
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
        [Route("GetTurnosByName")]
        public IActionResult GetTurnosByName(string turnoName)
        {
            try
            {
                turnoName = string.IsNullOrEmpty(turnoName) ? string.Empty : turnoName.Trim();
                turnoName = turnoName.Replace(' ', '%');
                turnoName = "%" + turnoName + "%";
                IEnumerable<Turnos> response = PosUoW.TurnosRepository.GetAllByCriteria(x => EF.Functions.Like(x.Nombre.ToLower(), turnoName.ToLower()), x => x.IdTurno);
                if (response != null)
                {
                    return Ok(response);
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
        [Route("SaveTurno")]
        public IActionResult SaveTurno([FromBody] Turnos turno)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (turno.IdTurno == 0)
                    {
                        PosUoW.TurnosRepository.Add(turno);
                    }
                    else
                    {
                        PosUoW.TurnosRepository.Update(turno);
                    }
                    PosUoW.Save();
                    return Ok(turno);
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
    }
}
