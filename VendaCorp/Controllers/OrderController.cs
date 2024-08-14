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

        /// <summary>
        /// Métdo que cria o Pedido
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
                if (response.IsFailed) return StatusCode(StatusCodes.Status400BadRequest, response);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        /// <summary>
        /// Método que muda o Status para "Aprovado"
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Método que muda Status do Pedido para "Cancelado"
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Método que busca Pedidos por Id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Método que busca todos os pedidos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                Claim idUser = User.FindFirst(ClaimTypes.NameIdentifier);
                if (idUser == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, ConstantsAuthorized.Error);
                }

                List<Order> response = await _orderService.GetAll();
                if (response == null) return StatusCode(StatusCodes.Status404NotFound, Result.Fail("Nenhum pedido localizado"));

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail("Erro ao buscar todos os pedidos"));
            }
        }
        /// <summary>
        /// Método uqe busca todos os pedidos filtrando por quantidade
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllFilteredByAmount")]
        public async Task<IActionResult> GetAllFilteredByAmountAsync(int amount)
        {
            try
            {
                Claim idUser = User.FindFirst(ClaimTypes.NameIdentifier);
                if (idUser == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, ConstantsAuthorized.Error);
                }

                List<Order> response = await _orderService.GetAllFilteredAmount(amount);
                if (response == null) return StatusCode(StatusCodes.Status404NotFound, Result.Fail("Nenhum pedido localizado"));

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail("Erro ao buscar todos os pedidos filtrado"));
            }
        }

    }
}
