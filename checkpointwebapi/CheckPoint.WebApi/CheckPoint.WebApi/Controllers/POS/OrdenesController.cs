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
    public class OrdenesController : BaseController
    {
        public OrdenesController(IOptions<DatabaseSettings> dbSettings, IHostingEnvironment environment) : base(dbSettings, environment)
        {
            DbSettings = dbSettings;
            Environment = environment;
        }

        [HttpGet]
        [Route("GetAllOrders")]
        public IActionResult GetAllOrders()
         {
            try
            {
                var ordersResult = PosUoW.OrdenesRepository.GetAllOrdersWithSupplier();
                if (ordersResult != null)
                {
                    return Ok(ordersResult);
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
        [Route("GetOrdersByEstatusWithSupplier")]
        public IActionResult GetOrdersByEstatusWithSupplier(char estatusSearch)
        {
            try
            {
                var ordersResult = PosUoW.OrdenesRepository.GetOrdersByEstatusWithSupplier(estatusSearch);
                if(ordersResult != null)
                {
                    return Ok(ordersResult);
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
        [Route("AddOrders")]
        public IActionResult AddOrders([FromBody] List<Ordenes> orderList)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PosUoW.OrdenesRepository.AddRange(orderList);
                    PosUoW.Save();
                    return Ok();
                }
                else
                {
                    return BadRequest("Los datos de las ordenes son incorrectos");
                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost]
        [Route("UpdateOrder")]
        public IActionResult UpdateOrder([FromBody] Ordenes orden)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PosUoW.OrdenesRepository.Update(orden);
                    PosUoW.Save();
                    return Ok();
                }
                else
                {
                    return BadRequest("Los datos de la orden son incorrectos");
                }
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
            
        }
    }
}
