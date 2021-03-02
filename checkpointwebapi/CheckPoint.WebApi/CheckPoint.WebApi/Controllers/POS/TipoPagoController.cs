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
    public class TipoPagoController : BaseController
    {
        public TipoPagoController(IOptions<DatabaseSettings> dbSettings, IHostingEnvironment environment) : base(dbSettings, environment)
        {
            DbSettings = dbSettings;
            Environment = environment;
        }
        
        [HttpGet]
        [Route("GetAllTipoPago")]
        public IActionResult GetAllTipoPago()
        {
            try
            {
                List<TipoPago> tipoPagoList = PosUoW.TipoPagoRepository.GetAllByCriteria(a => a.Estatus, a => a.idTipoPago).ToList();
                if(tipoPagoList != null)
                {
                    return Ok(tipoPagoList);
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

        [HttpGet]
        [Route("GetPaymentByName")]
        public IActionResult getPaymentByName(string name)
        {
            try
            {
                var departamento = PosUoW.TipoPagoRepository.GetPaymentByName(name).ToList();
                if (departamento != null)
                {
                    return Ok(departamento);
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
        [Route("GetTipoPagoById")]
        public IActionResult GetTipoPagoById(int idTipoPago)
        {
            try
            {
                TipoPago tipoPago = PosUoW.TipoPagoRepository.GetById(a => a.idTipoPago.Equals(idTipoPago));
                if(tipoPago != null)
                {
                    return Ok(tipoPago);
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

        [HttpPost]
        [Route("SavePayment")]
        public IActionResult SaveDepartamento([FromBody] TipoPago tipoPago)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (tipoPago.idTipoPago == 0)
                        PosUoW.TipoPagoRepository.Add(tipoPago);
                    else
                        PosUoW.TipoPagoRepository.Update(tipoPago);
                    PosUoW.Save();
                    return Ok(tipoPago);
                }
                else
                {
                    return BadRequest("Los datos del tipo de pago son incorrectos");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
