using CheckPoint.WebApi.Metadata;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Security.Business.Model;
using System;

namespace CheckPoint.WebApi.Controllers.POS
{
    public class UsuariosController : BaseController
    {
        public UsuariosController(IOptions<DatabaseSettings> dbSettings, IHostingEnvironment environment) : base(dbSettings, environment)
        {
            DbSettings = dbSettings;
            Environment = environment;
        }

        [HttpGet]
        [Route("GetUsuarioByUserName")]
        public IActionResult GetUsuarioByUserName(string userName)
        {
            try
            {
                Usuarios user = SecurityUoW.UsuariosRepository.GetUsuarioByUserName(userName);
                if (user != null)
                {
                    return Ok(user);
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
        [Route("GetUserAdmin")]
        public IActionResult GetUserAmin(string  user, string pass)
        {
            try
            {
                bool resul = PosUoW.UsuariosRepository.GetUsuarioAdmin(user, pass);
                if (resul == true)
                {
                    return Ok(true);
                }
                else
                {
                    return NotFound(false);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}