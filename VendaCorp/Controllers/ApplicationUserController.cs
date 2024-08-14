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

                return StatusCode(StatusCodes.Status201Created, response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound);
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

                return StatusCode(StatusCodes.Status200OK, response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound);
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

                return StatusCode(StatusCodes.Status200OK, response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound);
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

                return StatusCode(StatusCodes.Status200OK, response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound);
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

                return StatusCode(StatusCodes.Status200OK, response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

    }
}
