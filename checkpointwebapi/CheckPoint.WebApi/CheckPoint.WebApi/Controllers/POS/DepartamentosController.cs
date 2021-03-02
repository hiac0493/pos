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

namespace CheckPoint.WebApi.Controllers
{
    [Authorize]
    public class DepartamentosController : BaseController
    {
        public DepartamentosController(IOptions<DatabaseSettings> dbSettings, IHostingEnvironment environment) : base(dbSettings, environment)
        {
            DbSettings = dbSettings;
            Environment = environment;
        }

        [HttpGet]
        [Route("GetAllDepartamentos")]
        public IActionResult GetAllDepartamentos()
        {
            try
            {
                List<Departamentos> departamentosList = PosUoW.DepartamentosRepository.GetAllByCriteria(a => a.Estatus,a => a.idDepartamento).ToList();
                if (departamentosList != null)
                {
                    return Ok(departamentosList);
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
        [Route("GetDepartamentoById")]
        public IActionResult GetDepartamentoById(int id)
        {
            try
            {
                Departamentos departamento = PosUoW.DepartamentosRepository.GetById(a => a.idDepartamento.Equals(id));
                if(departamento != null)
                {
                    return Ok(departamento);
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
        [Route("getDepartmenByName")]
        public IActionResult getDepartmenByName(string name)
        {
            try
            {
                var departamento = PosUoW.DepartamentosRepository.getDepartmenByName(name).ToList();
                if(departamento != null)
                {
                    return Ok(departamento);
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
        [Route("SaveDepartamento")]
        public IActionResult SaveDepartamento([FromBody] Departamentos departamentos)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (departamentos.idDepartamento == 0)
                        PosUoW.DepartamentosRepository.Add(departamentos);
                    else
                        PosUoW.DepartamentosRepository.Update(departamentos);
                    PosUoW.Save();
                    return Ok(departamentos);
                }
                else
                {
                    return BadRequest("Los datos del departamento son incorrectos");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
