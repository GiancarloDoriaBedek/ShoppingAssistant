using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingAssistant.DTOs;
using ShoppingAssistant.Repository.Interfaces;
using System.Data;
using System.Net;

namespace ShoppingAssistant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "User,Admin")]
    public class PackageController : Controller
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IMapper _mapper;

        private ApiResponseDTO _response;

        public PackageController(IPackageRepository packageRepository, IMapper mapper)
        {
            _packageRepository  = packageRepository;
            _mapper = mapper;

            _response = new ApiResponseDTO();
        }

        [HttpGet("{id:long}", Name = "GetPriceHistory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponseDTO>> GetPriceHistory(long id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var packages = await _packageRepository.GetAllAsync(x => x.ProductID == id);
                if (!packages.Any())
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = packages;
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
