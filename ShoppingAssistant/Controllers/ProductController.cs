using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingAssistant.Models;
using ShoppingAssistant.Repository.Interfaces;

namespace ShoppingAssistant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Product>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetAllAsync();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(products);
        }


    }
}
