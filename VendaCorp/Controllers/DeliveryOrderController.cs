using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VendaCorp.Application.DTO.DeliveryOrder;
using VendaCorp.Application.DTO.Enterprise;
using VendaCorp.Core.ConstantsMessage;
using VendaCorp.Core.Entities;
using VendaCorp.Core.Interfaces.Services;

namespace VendaCorp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeliveryOrderController : ControllerBase
    {
        private readonly IDeliveryOrderService _deliveryService;

        public DeliveryOrderController(IDeliveryOrderService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        /// <summary>
        /// Método que cria o o Pedido de Entrega
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] DeliveryOrderCreateVO model)
        {
            try
            {
                //Claim idUser = User.FindFirst(ClaimTypes.NameIdentifier);
                //if (idUser == null)
                //{
                //    return StatusCode(StatusCodes.Status401Unauthorized);
                //}

                Result response = await _deliveryService.CreateAsync(model);
                if (response.IsFailed) return StatusCode(StatusCodes.Status400BadRequest, Result.Ok("Sucesso ao criar pedido de entrega"));

                return StatusCode(StatusCodes.Status201Created, Result.Fail("Erro ao criar pedido de entrega"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Método que busca todos Pedido de entrega
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

                List<DeliveryOrder> deliveries = await _deliveryService.GetAllAsync();

                return StatusCode(StatusCodes.Status200OK, deliveries);
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail("Erro ao buscar todas as entregas dos pedidos"));
            }
        }


        /// <summary>
        /// Método que muda Status de Pedido de Entrega para "A caminho"
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("StatusOnTheWay")]
        public async Task<IActionResult> StatusOnTheWayAsync(int deliveryId)
        {
            try
            {
                Claim idUser = User.FindFirst(ClaimTypes.NameIdentifier);
                if (idUser == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, ConstantsAuthorized.Error);
                }

                Result response = await _deliveryService.OnTheWayAsync(deliveryId);
                if(response.IsFailed) return StatusCode(StatusCodes.Status400BadRequest, Result.Fail("Erro ao mudar status da entrega para 'A caminho'!"));

                return StatusCode(StatusCodes.Status200OK, "Pedido a caminho");
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail("Erro ao mudar status da entrega para 'A caminho'!"));
            }
        }

        /// <summary>
        /// Método que muda Status de Pedido de Entrega para "Entregue"
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("StatusDelivered")]
        public async Task<IActionResult> StatusDeliveredAsync(int deliveryId)
        {
            try
            {
                Claim idUser = User.FindFirst(ClaimTypes.NameIdentifier);
                if (idUser == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, ConstantsAuthorized.Error);
                }

                Result response = await _deliveryService.DeliveredAsync(deliveryId);
                if (response.IsFailed) return StatusCode(StatusCodes.Status400BadRequest, Result.Fail("Erro ao mudar status da entrega para 'Entregue'!"));

                return StatusCode(StatusCodes.Status200OK, "Pedido entregue");
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail("Erro ao mudar status da entrega para 'Entregue'!"));
            }
        }
    }
}
