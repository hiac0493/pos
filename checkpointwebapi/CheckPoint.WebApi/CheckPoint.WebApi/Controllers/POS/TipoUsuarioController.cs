using CheckPoint.WebApi.Metadata;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Pos.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckPoint.WebApi.Controllers.POS
{
    public class TipoUsuarioController:BaseController
    {
        public TipoUsuarioController(IOptions<DatabaseSettings> dbSettings, IHostingEnvironment environment) : base(dbSettings, environment)
        {
            DbSettings = dbSettings;
            Environment = environment;
        }

        [HttpGet]
        [Route("GetAllUserTypes")]
        public IActionResult GetAllUserTypes()
        {
            try
            {
                List<TipoUsuario> tipoUsuariosResult = PosUoW.TipoUsuarioRepository.GetAll(x => x.idTipoUsuario).ToList();
                if (tipoUsuariosResult != null)
                    return Ok(tipoUsuariosResult);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
