using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using VendaCorp.Application.DTO.Enterprise;
using VendaCorp.Application.DTO.Order;
using VendaCorp.Core.ConstantsMessage;
using VendaCorp.Core.Entities;
using VendaCorp.Core.Interfaces.Services;
using VendaCorp.Infrastructure.Services;

namespace VendaCorp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] OrderCreateVO model)
        {
            try
            {
                //Claim idUser = User.FindFirst(ClaimTypes.NameIdentifier);
                //if (idUser == null)
                //{
                //    return StatusCode(StatusCodes.Status401Unauthorized);
                //}

                Result response = await _orderService.CreateAsync(model);
                if (response.IsFailed) return StatusCode(StatusCodes.Status400BadRequest);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("ApprovedAsync")]
        public async Task<IActionResult> ApproveAsync(string orderId)
        {
            try
            {
                Claim idUser = User.FindFirst(ClaimTypes.NameIdentifier);
                if (idUser == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, ConstantsAuthorized.Error);
                }

                Result response = await _orderService.ApproveAsync(orderId);
                if (response.IsFailed) return StatusCode(StatusCodes.Status400BadRequest);

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("CancellAsync")]
        public async Task<IActionResult> CancellAsync(string orderId)
        {
            try
            {
                Claim idUser = User.FindFirst(ClaimTypes.NameIdentifier);
                if (idUser == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, ConstantsAuthorized.Error);
                }

                Result response = await _orderService.CancellAsync(orderId);
                if (response.IsFailed) return StatusCode(StatusCodes.Status400BadRequest);

                return StatusCode(StatusCodes.Status200OK);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetByIdAsync(string orderId)
        {
            try
            {
                Claim idUser = User.FindFirst(ClaimTypes.NameIdentifier);
                if (idUser == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, ConstantsAuthorized.Error);
                }

                Order response = await _orderService.GetById(orderId);
                if (response == null) return StatusCode(StatusCodes.Status404NotFound);

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
