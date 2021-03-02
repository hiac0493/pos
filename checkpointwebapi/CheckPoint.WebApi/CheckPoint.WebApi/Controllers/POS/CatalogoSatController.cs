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
    public class CatalogoSatController : BaseController
    {
        public CatalogoSatController(IOptions<DatabaseSettings> dbSettings, IHostingEnvironment environment) : base(dbSettings, environment)
        {
            DbSettings = dbSettings;
            Environment = environment;
        }

        [HttpGet]
        [Route("GetAllCatalogoSat")]
        public IActionResult GetAllCatalogoSat()
        {
            try
            {
                List<CatalogoSat> catalogoSatList = PosUoW.CatalogoSatRepository.GetAllByCriteria(x => x.Activo, x => x.idCatalogoSat).ToList();
                if (catalogoSatList != null)
                {
                    return Ok(catalogoSatList);
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
        [Route("GetCatalogByName")]
        public IActionResult GetCatalogByName(string catalogName)
        {
            try
            {
                catalogName = string.IsNullOrEmpty(catalogName) ? string.Empty : catalogName.Trim();
                catalogName = catalogName.Replace(' ', '%');
                catalogName = "%" + catalogName + "%";
                IEnumerable<CatalogoSat> catalogResult = PosUoW.CatalogoSatRepository.GetAllByCriteria(x => EF.Functions.Like(x.Descripcion.ToLower(), catalogName.ToLower()), x => x.idCatalogoSat);
                if (catalogResult != null)
                {
                    return Ok(catalogResult);
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
        [Route("SaveCatalogSat")]
        public IActionResult SaveCatalogSat([FromBody] CatalogoSat catalog)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (catalog.idCatalogoSat == 0)
                    {
                        PosUoW.CatalogoSatRepository.Add(catalog);
                    }
                    else
                    {
                        PosUoW.CatalogoSatRepository.Update(catalog);
                    }
                    PosUoW.Save();
                    return Ok(catalog);
                }
                else
                {
                    return BadRequest("Los datos del  catalogo son incorrectos");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
