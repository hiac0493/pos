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
    public class ImpuestosController : BaseController
    {
        public ImpuestosController(IOptions<DatabaseSettings> dbSettings, IHostingEnvironment environment) : base(dbSettings, environment)
        {
            DbSettings = dbSettings;
            Environment = environment;
        }

        [HttpGet]
        [Route("GetAllImpuestos")]
        public IActionResult GetAllImpuestos()
        {
            try
            {  
                List<Impuestos> impuestosList = PosUoW.ImpuestosRepository.GetAllByCriteria(a => a.Estatus, a => a.idImpuesto).ToList();
                if(impuestosList != null)
                {
                    return Ok(impuestosList);
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
        [Route("GetImpuestoById")]
        public IActionResult GetImpuestoById(int id)
        {
            try
            {
                Impuestos impuesto = PosUoW.ImpuestosRepository.GetById(a => a.idImpuesto.Equals(id));
                if(impuesto!= null)
                {
                    return Ok(impuesto);
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

        [HttpDelete]
        [Route("DeleteImpuestoProductoById")]
        public IActionResult DeleteImpuestoProductoById(int idImpuestoProducto)
        {
            ImpuestoProductos producto = PosUoW.ImpuestoProductosRepository.GetById(x => x.idImpuestoProducto.Equals(idImpuestoProducto));
            try
            {
                if (producto != null)
                {
                    PosUoW.ImpuestoProductosRepository.Remove(producto);
                    PosUoW.Save();
                    return Ok();
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
        [Route("GetImpuestoByName")]
        public IActionResult GetImpuestoByName(string name)
        {
            try
            {
                var impuesto = PosUoW.ImpuestosRepository.getImpuestoByName(name).ToList();
                if (impuesto != null)
                {
                    return Ok(impuesto);
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
        [Route("SaveImpuesto")]
        public IActionResult SaveImpuesto([FromBody] Impuestos impuestos)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (impuestos.idImpuesto == 0)
                        PosUoW.ImpuestosRepository.Add(impuestos);
                    else
                        PosUoW.ImpuestosRepository.Update(impuestos);
                    PosUoW.Save();
                    return Ok(impuestos);
                }
                else
                {
                    return BadRequest("Los datos del impuesto son incorrectos");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
