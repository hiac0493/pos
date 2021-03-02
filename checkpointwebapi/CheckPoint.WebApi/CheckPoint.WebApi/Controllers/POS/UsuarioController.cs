using CheckPoint.WebApi.Metadata;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pos.Business.Model;
using System;
using System.Linq;

namespace CheckPoint.WebApi.Controllers.POS
{
    public class UsuarioController : BaseController
    {
        public UsuarioController(IOptions<DatabaseSettings> dbSettings, IHostingEnvironment environment) : base(dbSettings, environment)
        {
            DbSettings = dbSettings;
            Environment = environment;
        }

        [HttpGet]
        [Route("GetUsuarioByName")]
        public IActionResult GetUsuarioByName(string name)
        {
            try
            {
                var user = PosUoW.UsuariosRepository.GetUsuarioByName(name).ToList();
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
        [Route("GetAllUsuarios")]
        public IActionResult GetAllUsuarios()
        {
            try
            {
                var users = PosUoW.UsuariosRepository.GetAllByCriteria(x => x.Activo, x => x.idUsuario).ToList();
                if (users != null)
                {
                    return Ok(users);
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
        [Route("SaveUser")]
        public IActionResult SaveUser([FromBody] Usuarios usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (usuario.idUsuario == 0)
                        PosUoW.UsuariosRepository.Add(usuario);
                    else
                        PosUoW.UsuariosRepository.Update(usuario);
                    PosUoW.Save();
                    return Ok(usuario);
                }
                else
                {
                    return BadRequest("Los datos de la venta son incorrectos");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
