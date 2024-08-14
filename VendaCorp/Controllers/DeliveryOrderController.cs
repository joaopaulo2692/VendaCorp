﻿using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VendaCorp.Application.DTO.DeliveryOrder;
using VendaCorp.Application.DTO.Enterprise;
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
                if (response.IsFailed) return StatusCode(StatusCodes.Status400BadRequest, response);

                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                Claim idUser = User.FindFirst(ClaimTypes.NameIdentifier);
                if (idUser == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }

                List<DeliveryOrder> deliveries = await _deliveryService.GetAllAsync();

                return StatusCode(StatusCodes.Status200OK, deliveries);
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail("Erro ao buscar todas as entregas dos pedidos"));
            }
        }


        [HttpPost]
        [Route("StatusOnTheWay")]
        public async Task<IActionResult> StatusOnTheWayAsync(int deliveryId)
        {
            try
            {
                Claim idUser = User.FindFirst(ClaimTypes.NameIdentifier);
                if (idUser == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }

                Result response = await _deliveryService.OnTheWayAsync(deliveryId);
                if(response.IsFailed) return StatusCode(StatusCodes.Status400BadRequest, Result.Fail("Erro ao mudar status da entrega para 'A caminho'!"));

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail("Erro ao mudar status da entrega para 'A caminho'!"));
            }
        }

        [HttpPost]
        [Route("StatusDelivered")]
        public async Task<IActionResult> StatusDeliveredAsync(int deliveryId)
        {
            try
            {
                Claim idUser = User.FindFirst(ClaimTypes.NameIdentifier);
                if (idUser == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized);
                }

                Result response = await _deliveryService.DeliveredAsync(deliveryId);
                if (response.IsFailed) return StatusCode(StatusCodes.Status400BadRequest, Result.Fail("Erro ao mudar status da entrega para 'Entregue'!"));

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail("Erro ao mudar status da entrega para 'Entregue'!"));
            }
        }
    }
}
