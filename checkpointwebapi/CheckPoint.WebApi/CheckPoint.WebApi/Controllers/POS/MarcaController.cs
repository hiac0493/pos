using CheckPoint.WebApi.Metadata;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class MarcaController : BaseController
    {
        public MarcaController(IOptions<DatabaseSettings> dbSettings, IHostingEnvironment environment) : base(dbSettings, environment)
        {
            DbSettings = dbSettings;
            Environment = environment;
        }

        [HttpGet]
        [Route("GetAllMarcas")]
        public IActionResult GetAllMarcas()
        {
            try
            {
                List<Marca> marcaList = PosUoW.MarcaRepository.GetAllByCriteria(x => x.Activo, x => x.idMarca).ToList();
                if (marcaList != null)
                {
                    return Ok(marcaList);
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
        [Route("GetBrandsByName")]
        public IActionResult GetBrandsByName(string brandName)
        {
            try
            {
                brandName = string.IsNullOrEmpty(brandName) ? string.Empty : brandName.Trim();
                brandName = brandName.Replace(' ', '%');
                brandName = "%" + brandName + "%";
                IEnumerable<Marca> brandsResult = PosUoW.MarcaRepository.GetAllByCriteria(x=>EF.Functions.Like(x.Descripcion.ToLower(), brandName.ToLower()), x=>x.idMarca);
                if(brandsResult != null)
                {
                    return Ok(brandsResult);
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
        [Route("SaveBrand")]
        public IActionResult SaveBrand([FromBody] Marca marca)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (marca.idMarca == 0)
                    {
                        PosUoW.MarcaRepository.Add(marca);
                    }
                    else
                    {
                        PosUoW.MarcaRepository.Update(marca);
                    }
                    PosUoW.Save();
                    return Ok(marca);
                }
                else
                {
                    return BadRequest("Los datos de la marca son incorrectos");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
