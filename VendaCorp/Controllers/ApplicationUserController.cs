using FluentResults;
using Microsoft.AspNetCore.Mvc;
using VendaCorp.Application.DTO.ApplicationUser;
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

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> DeleteUser(string idUser)
        {

            try
            {
                Result response = await _userService.DeleteAsync(idUser);

                return StatusCode(StatusCodes.Status200OK, response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {

            try
            {
                List<UserVO> response = await _userService.GetAllAsync();

                return StatusCode(StatusCodes.Status200OK, response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(string idUser)
        {

            try
            {
                UserVO response = await _userService.GetByIdAsync(idUser);

                return StatusCode(StatusCodes.Status200OK, response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

    }
}
