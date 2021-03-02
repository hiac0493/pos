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

namespace CheckPoint.WebApi.Controllers.POS
{
    public class UnidadSatController : BaseController
    {
        public UnidadSatController(IOptions<DatabaseSettings> dbSettings, IHostingEnvironment environment) : base(dbSettings, environment)
        {
            DbSettings = dbSettings;
            Environment = environment;
        }

        [HttpGet]
        [Route("GetAllUnidadesSat")]
        public IActionResult GetAllUnidadesSat()
        {
            try
            {
                List<UnidadSat> unidadSatList = PosUoW.UnidadSatRepository.GetAllByCriteria(x => x.Activo, x => x.idUnidadSat).ToList();
                if (unidadSatList != null)
                {
                    return Ok(unidadSatList);
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
        [Route("GetUnidadesSatByid")]
        public IActionResult GetUnidadesSatByid(int unitSatId)
        {
            try
            {
                var unidadSatList = PosUoW.UnidadSatRepository.GetUnidadesSatByid(unitSatId).ToList();
                if (unidadSatList != null)
                {
                    return Ok(unidadSatList);
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
        [Route("GetUnitsSatByName")]
        public IActionResult GetUnitsSatByName(string unitName)
        {
            try
            {
                unitName = string.IsNullOrEmpty(unitName) ? string.Empty : unitName.Trim();
                unitName = unitName.Replace(' ', '%');
                unitName = "%" + unitName + "%";
                IEnumerable<UnidadSat> unitsResult = PosUoW.UnidadSatRepository.GetAllByCriteria(x => EF.Functions.Like(x.Descripcion.ToLower(), unitName.ToLower()), x => x.idUnidadSat);
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
        [Route("SaveUnitSat")]
        public IActionResult SaveUnitSat([FromBody] UnidadSat unidad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (unidad.idUnidadSat == 0)
                    {
                        PosUoW.UnidadSatRepository.Add(unidad);
                    }
                    else
                    {
                        PosUoW.UnidadSatRepository.Update(unidad);
                    }
                    PosUoW.Save();
                    return Ok(unidad);
                }
                else
                {
                    return BadRequest("Los datos de la unidad sat son incorrectos");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
