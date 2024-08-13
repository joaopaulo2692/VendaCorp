using FluentResults;
using Microsoft.AspNetCore.Mvc;
using VendaCorp.Application.DTO.DeliveryOrder;
using VendaCorp.Application.DTO.Enterprise;
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
    }
}
