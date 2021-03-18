using CheckPoint.WebApi.Metadata;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Security.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CheckPoint.WebApi.Controllers.UserSecurity
{
    public class PantallasController : BaseController
    {
        public PantallasController(IOptions<DatabaseSettings> dbSettings, IHostingEnvironment environment) : base(dbSettings, environment)
        {
            DbSettings = dbSettings;
            Environment = environment;
        }

        [HttpGet]
        [Route("GetAllPantallas")]
        public IActionResult GetAllPantallas()
        {
            try
            {
                List<Pantallas> screensResult = SecurityUoW.PantallasRepository.GetAllByCriteria(x => x.Activo, x => x.idPantalla).ToList();
                if (screensResult != null)
                    return Ok(screensResult);
                else
                    return NotFound();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("GetAllPantallasByUser")]
        public IActionResult GetAllPantallasByUser(int idUsuario)
        {
            try
            {
                List<Pantallas> screensUser = SecurityUoW.PantallasRepository.GetPantallasByUserId(idUsuario).ToList();
                if (screensUser != null)
                {
                    return Ok(screensUser);
                }
                else
                    return NotFound();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("GetAllPrincipalPantallasByUser")]
        public IActionResult GetAllPrincipalPantallasByUser(int idUsuario)
        {
            try
            {
                List<Pantallas> screensResult = SecurityUoW.PantallasRepository.GetAllPrincipalPantallasByUser(idUsuario).ToList();
                if (screensResult != null)
                    return Ok(screensResult);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("GetAllPrincipalPantallas")]
        public IActionResult GetAllPrincipalPantallas()
        {
            try
            {
                List<Pantallas> screensResult = SecurityUoW.PantallasRepository.GetAllByCriteria(x => x.Activo && !x.SubPantalla, x => x.idPantalla).ToList();
                if (screensResult != null)
                    return Ok(screensResult);
                else
                    return NotFound();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("GetAllSubPantallas")]
        public IActionResult GetAllSubPantallas()
        {
            try
            {
                List<Pantallas> subScreensResult = SecurityUoW.PantallasRepository.GetAllByCriteria(x => x.Activo && x.SubPantalla, x => x.idPantalla).ToList();
                if (subScreensResult != null)
                    return Ok(subScreensResult);
                else
                    return NotFound();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet]
        [Route("GetScreenByName")]
        public IActionResult GetScreenByName(string screenName)
        {
            try
            {
                screenName = string.IsNullOrEmpty(screenName) ? string.Empty : screenName.Trim();
                screenName = screenName.Replace(' ', '%');
                screenName = "%" + screenName + "%";
                IEnumerable<Pantallas> response = SecurityUoW.PantallasRepository.GetAllByCriteria(x => EF.Functions.Like(x.NombrePantalla.ToLower(), screenName.ToLower()), x => x.idPantalla);
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
        [Route("SaveScreen")]
        public IActionResult SaveScreen([FromBody] Pantallas pantalla)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (pantalla.idPantalla == 0)
                    {
                        SecurityUoW.PantallasRepository.Add(pantalla);
                    }
                    else
                    {
                        SecurityUoW.PantallasRepository.Update(pantalla);
                    }
                    SecurityUoW.Save();
                    return Ok(pantalla);
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
