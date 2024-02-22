using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingAssistant.DTOs;
using ShoppingAssistant.DTOs.ProductDTOs;
using ShoppingAssistant.Models;
using ShoppingAssistant.Repository.Interfaces;
using ShoppingAssistant.Services.Interfaces;
using System.Net;

namespace ShoppingAssistant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "User,Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        private ApiResponseDTO _response;

        public ProductController(IProductService productService)
        {
            _productService = productService;

            _response = new ApiResponseDTO();
        }

        //[HttpGet("{name}", Name = "GetProductsByName")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult<ApiResponseDTO>> GetProducts([FromQuery] string name)
        //{
        //    try
        //    {
        //        if (name is null)
        //        {
        //            _response.IsSuccess = false;
        //            _response.StatusCode = HttpStatusCode.BadRequest;
        //            return BadRequest(_response);
        //        }

        //        var productsOfSimilarName = await _productRepository.GetProductsByName(name);

        //        if (!productsOfSimilarName.Any())
        //        {
        //            _response.IsSuccess = false;
        //            _response.StatusCode = HttpStatusCode.NotFound;
        //            return NotFound(_response);
        //        }

        //        _response.Result = productsOfSimilarName;
        //        _response.StatusCode = HttpStatusCode.OK;

        //        return Ok(_response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.StatusCode = HttpStatusCode.InternalServerError;
        //        _response.ErrorMessages.Add(ex.Message);

        //        return StatusCode(StatusCodes.Status500InternalServerError, _response);
        //    }
        //}

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponseDTO>> GetProducts([FromQuery] GetProductsRequestDTO filter)
        {
            try
            {
                if (filter is null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var products = await _productService.GetProducts(filter);

                if (!products.Any())
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = products;
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add(ex.Message);

                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }
    }
}
