using CheckPoint.WebApi.Metadata;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class UnidadesController : BaseController
    {
        public UnidadesController(IOptions<DatabaseSettings> dbSettings, IHostingEnvironment environment) : base(dbSettings, environment)
        {
            DbSettings = dbSettings;
            Environment = environment;
        }

        [HttpGet]
        [Route("GetAllUnidades")]
        public IActionResult GetAllUnidades()
        {
            try
            {
                List<Unidades> unidadesList = PosUoW.UnidadesRepository.GetAllByCriteria(x => x.Activo, x => x.idUnidad).ToList();
                if (unidadesList != null)
                {
                    return Ok(unidadesList);
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
        [Route("GetUnitsByName")]
        public IActionResult GetUnitsByName(string unitName)
        {
            try
            {
                unitName = string.IsNullOrEmpty(unitName) ? string.Empty : unitName.Trim();
                unitName = unitName.Replace(' ', '%');
                unitName = "%" + unitName + "%";
                IEnumerable<Unidades> unitsResult = PosUoW.UnidadesRepository.GetAllByCriteria(x => EF.Functions.Like(x.Descripcion.ToLower(), unitName.ToLower()), x => x.idUnidad);
                if (unitsResult != null)
                {
                    return Ok(unitsResult);
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
        [Route("SaveUnit")]
        public IActionResult SaveUnit([FromBody] Unidades unidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (unidad.idUnidad == 0)
                    {
                        PosUoW.UnidadesRepository.Add(unidad);
                    }
                    else
                    {
                        PosUoW.UnidadesRepository.Update(unidad);
                    }
                    PosUoW.Save();
                    return Ok(unidad);
                }
                else
                {
                    return BadRequest("Los datos de la unidad son incorrectos");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
