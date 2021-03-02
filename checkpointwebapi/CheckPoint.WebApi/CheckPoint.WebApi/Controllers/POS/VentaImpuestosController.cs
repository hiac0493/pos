using CheckPoint.WebApi.Metadata;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckPoint.WebApi.Controllers.POS
{
    public class VentaImpuestosController : BaseController
    {
        public VentaImpuestosController(IOptions<DatabaseSettings> dbSettings, IHostingEnvironment environment) : base(dbSettings, environment)
        {
            DbSettings = dbSettings;
            Environment = environment;
        }

        [HttpGet]
        [Route("GetAllTaxes")]
        public IActionResult GetAllTaxes(long folioInicio, long folioFinal, int IdUsuario)
        {
            try
            {
                var response = PosUoW.VentaImpuestosRepository.GetAllTaxes(folioInicio, folioFinal, IdUsuario);
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

        [HttpGet]
        [Route("GetTotalTaxes")]
        public IActionResult GetTotalTaxes(long folioInicio, long folioFinal, int IdUsuario)
        {
            try
            {
                double response = PosUoW.VentaImpuestosRepository.GetTotalTaxes(folioInicio, folioFinal, IdUsuario);
                if (response != 0)
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
    }
}
