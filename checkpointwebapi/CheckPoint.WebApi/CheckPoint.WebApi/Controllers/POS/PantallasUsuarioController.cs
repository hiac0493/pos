using CheckPoint.WebApi.Metadata;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pos.Business.Model;
using System;

namespace CheckPoint.WebApi.Controllers.POS
{
    public class PantallasUsuarioController : BaseController
    {
        public PantallasUsuarioController(IOptions<DatabaseSettings> dbSettings, IHostingEnvironment environment) : base(dbSettings, environment)
        {
            DbSettings = dbSettings;
            Environment = environment;
        }

        [HttpDelete]
        [Route("DeletePantallaUsuario")]
        public IActionResult DeletePantallaUsuario(long idPantallaUsuario)
        {
            try
            {
                PantallasUsuario screenToRemove = PosUoW.PantallasUsuarioRepository.GetById(x => x.idPantallasUsuario.Equals(idPantallaUsuario));
                if(screenToRemove != null)
                {
                    PosUoW.PantallasUsuarioRepository.Remove(screenToRemove);
                    PosUoW.Save();
                    return Ok();
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

    }
}
