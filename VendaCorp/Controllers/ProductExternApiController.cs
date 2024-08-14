using Microsoft.AspNetCore.Mvc;
using VendaCorp.Application.DTO;
using VendaCorp.Core.Interfaces.Services;

namespace VendaCorp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductExternApiController : ControllerBase
    {
        private readonly IProductExternApiService _productExtern;

        public ProductExternApiController(IProductExternApiService productExtern)
        {
            _productExtern = productExtern;
        }

        /// <summary>
        /// Método que retorna todos os produtos da API externa
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<ProductVO> products = await _productExtern.GetAll();
                if (products == null) return StatusCode(StatusCodes.Status404NotFound);

                return StatusCode(StatusCodes.Status200OK, products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
