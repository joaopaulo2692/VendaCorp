using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VendaCorp.Application.DTO.ApplicationUser;
using VendaCorp.Core.ConstantsMessage;
using VendaCorp.Core.Interfaces.Services;

namespace VendaCorp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationUserController : ControllerBase
    {
        private readonly IApplicationUserService _userService;

        public ApplicationUserController(IApplicationUserService userService)
        {
            _userService = userService;
        }


        /// <summary>
        /// Método que cria novo Usuário
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> SaveUser([FromBody] UserCreateVO user)
        {

            try
            {
                Result response = await _userService.CreateAsync(user);

                return StatusCode(StatusCodes.Status201Created, Result.Ok("Sucesso ao criar usuário"));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, Result.Fail("Erro ao criar usuário"));
            }
        }

        /// <summary>
        /// Método que deleta Usuário
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> DeleteUser(string idUser)
        {

            try
            {
                Claim isLogged = User.FindFirst(ClaimTypes.NameIdentifier);
                if (isLogged == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, ConstantsAuthorized.Error);
                }
                Result response = await _userService.DeleteAsync(idUser);

                return StatusCode(StatusCodes.Status200OK, Result.Ok("Sucesso ao deletar usuário"));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, Result.Fail("Erro ao deletar usuário"));
            }
        }

        /// <summary>
        /// Método que busca todos os Usuários
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {

            try
            {
                Claim isLogged = User.FindFirst(ClaimTypes.NameIdentifier);
                if (isLogged == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, ConstantsAuthorized.Error);
                }
                List<UserVO> response = await _userService.GetAllAsync();

                return StatusCode(StatusCodes.Status200OK, Result.Ok("Sucesso ao buscar todos usuário"));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, Result.Fail("Erro ao buscar todos usuário"));
            }
        }
        /// <summary>
        /// Método para buscar Usuários por Id
        /// </summary>
        /// <param name="idUser"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(string idUser)
        {

            try
            {
                Claim isLogged = User.FindFirst(ClaimTypes.NameIdentifier);
                if (isLogged == null)
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, ConstantsAuthorized.Error);
                }
                UserVO response = await _userService.GetByIdAsync(idUser);
                if(response == null) return StatusCode(StatusCodes.Status404NotFound, Result.Fail("Erro ao buscar usuário"));

                return StatusCode(StatusCodes.Status200OK, Result.Ok("Sucesso ao buscar usuário"));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, Result.Fail("Erro ao buscar usuário"));
            }
        }

        /// <summary>
        /// Método para Fazer Autenticação
        /// </summary>
        /// <param name="loginVO"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginVO loginVO)
        {

            try
            {
                Result response = await _userService.Login(loginVO);
                if(response.IsFailed) return StatusCode(StatusCodes.Status400BadRequest, Result.Fail("Erro ao fazer login"));

                return StatusCode(StatusCodes.Status200OK, Result.Ok("Sucesso ao fazer login"));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, Result.Fail("Erro ao fazer login"));
            }
        }

    }
}
