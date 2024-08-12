using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VendaCorp.Application.DTO;
using VendaCorp.Application.DTO.Enterprise;
using VendaCorp.Core.ConstantsMessage;
using VendaCorp.Core.Interfaces.Repositories;
using VendaCorp.Core.Interfaces.Services;

namespace VendaCorp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnterpriseController : ControllerBase
    {
        private readonly IEnterpriseService _enterpriseService;

        public EnterpriseController(IEnterpriseService enterpriseService)
        {
            _enterpriseService = enterpriseService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] EnterpriseVO model)
        {
            try
            {
                //Claim idUser = User.FindFirst(ClaimTypes.NameIdentifier);
                //if (idUser == null)
                //{
                //    return StatusCode(StatusCodes.Status401Unauthorized);
                //}

                Result response = await _enterpriseService.CreateAsync(model);
                if (response.IsFailed) return StatusCode(StatusCodes.Status400BadRequest);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("Activate")]
        public async Task<IActionResult> ActivateAsync(int id)
        {
            try
            {
                Claim idUser = User.FindFirst(ClaimTypes.NameIdentifier);
                if (idUser == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, ConstantsAuthorized.Error);
                }

                Result response = await _enterpriseService.ActivateAsync(id);
                if (response.IsFailed) return StatusCode(StatusCodes.Status400BadRequest);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("Disable")]
        public async Task<IActionResult> DisableAsync(int id)
        {
            try
            {
                Claim idUser = User.FindFirst(ClaimTypes.NameIdentifier);
                if (idUser == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, ConstantsAuthorized.Error);
                }

                Result response = await _enterpriseService.DisableAsync(id);
                if (response.IsFailed) return StatusCode(StatusCodes.Status400BadRequest);

                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                Claim idUser = User.FindFirst(ClaimTypes.NameIdentifier);
                if (idUser == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, ConstantsAuthorized.Error);
                }

                EnterpriseVO response = await _enterpriseService.GetById(id);
                if (response == null) return StatusCode(StatusCodes.Status404NotFound);

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpGet]
        [Route("GetByLegalName")]
        public async Task<IActionResult> GetByLegalNameAsync(string legalName)
        {
            try
            {
                Claim idUser = User.FindFirst(ClaimTypes.NameIdentifier);
                if (idUser == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, ConstantsAuthorized.Error);
                }

                EnterpriseVO response = await _enterpriseService.GetByLegalName(legalName);
                if (response == null) return StatusCode(StatusCodes.Status404NotFound);

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetByTradeName")]
        public async Task<IActionResult> GetByTradeNameAsync(string tradeName)
        {
            try
            {
                Claim idUser = User.FindFirst(ClaimTypes.NameIdentifier);
                if (idUser == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, ConstantsAuthorized.Error);
                }

                EnterpriseVO response = await _enterpriseService.GetByTradeName(tradeName);
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
