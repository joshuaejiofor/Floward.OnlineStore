using AutoMapper;
using Floward.OnlineStore.ApplicationCore.Services.Interfaces;
using Floward.OnlineStore.Core.Helpers.Interfaces;
using Floward.OnlineStore.Core.Models;
using Floward.OnlineStore.Core.UnitOfWork;
using Floward.OnlineStore.WebApi.Controllers.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;

namespace Floward.OnlineStore.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/order")]
    [Produces("application/json")]
    public class OrderApiController : ApiControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderApiController(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger, IConfiguration configuration,
                IUriHelper uriHelper, IOrderService orderService)
            : base(unitOfWork, mapper, logger, configuration, uriHelper) 
        {
            _orderService = orderService;
        }


        [HttpPost("addtocart")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult AddToCart(Order order)
        {
            try
            {
                _orderService.AddToCartAsync(order);
                return Ok();
            }
            catch (Exception ex)
            {
                var problem = ex.InnerException?.ToString() ?? ex.Message.ToString();
                return BadRequest(problem);
            }
        }

        [HttpPost("removefromcart")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public IActionResult RemoveFromCart(Order order)
        {
            try
            {
                _orderService.RemoveFromCartAsync(order);
                return Ok();
            }
            catch (Exception ex)
            {
                var problem = ex.InnerException?.ToString() ?? ex.Message.ToString();
                return BadRequest(problem);
            }
        }
    }
}
