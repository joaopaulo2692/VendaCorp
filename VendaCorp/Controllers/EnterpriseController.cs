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
        /// <summary>
        /// Método que cria Empresa
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] EnterpriseCreateVO model)
        {
            try
            {
                //Claim idUser = User.FindFirst(ClaimTypes.NameIdentifier);
                //if (idUser == null)
                //{
                //    return StatusCode(StatusCodes.Status401Unauthorized);
                //}

                Result response = await _enterpriseService.CreateAsync(model);
                if (response.IsFailed) return StatusCode(StatusCodes.Status400BadRequest, Result.Fail("Erro ao criar empresa"));

                return StatusCode(StatusCodes.Status201Created, Result.Ok("Sucesso ao criar empresa"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail("Erro ao criar empresa"));
            }
        }

        /// <summary>
        /// Método que Muda Status da Empresa para "Ativo"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

                return StatusCode(StatusCodes.Status200OK, Result.Ok("Sucesso ao ativar empresa"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail("Erro ao ativar empresa"));
            }
        }
        /// <summary>
        /// Método que Muda Status da Empresa para "Inativo"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
                if (response.IsFailed) return StatusCode(StatusCodes.Status400BadRequest, Result.Fail("Erro ao deletar empresa"));

                return StatusCode(StatusCodes.Status200OK, Result.Fail("Sucesso ao deletar empresa"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail("Erro ao deletar empresa"));
            }
        }

        /// <summary>
        /// Método que busca Empresa pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
                if (response.LegalName == null) return StatusCode(StatusCodes.Status404NotFound, Result.Fail("Erro ao buscar empresa por Id"));

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail("Erro ao buscar empresa por Id"));
            }
        }

        /// <summary>
        /// Sucesso ao buscar todas Empresas
        /// </summary>
        /// <param name="id"></param>
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

                List<EnterpriseVO> response = await _enterpriseService.GetAll();
                if (response == null || response.Count == 0) return StatusCode(StatusCodes.Status404NotFound, Result.Fail("Erro ao buscar empresas"));

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Result.Fail("Erro ao buscar empresas"));
            }
        }

        /// <summary>
        /// Método que busca Empresa pela Razão Social
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
                if (response.LegalName == null) return StatusCode(StatusCodes.Status404NotFound);

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Método que busca Empresa pelo nome Fantasia
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
                if (response.LegalName == null) return StatusCode(StatusCodes.Status404NotFound);

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
