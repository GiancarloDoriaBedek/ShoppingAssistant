using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingAssistant.DTOs;
using ShoppingAssistant.DTOs.RoleDTOs;
using ShoppingAssistant.DTOs.UserDTOs;
using ShoppingAssistant.Models;
using ShoppingAssistant.Repository.Interfaces;
using System.Net;

namespace ShoppingAssistant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        private ApiResponseDTO _response;

        public RoleController(
            IRoleRepository roleRepository,
            IUserRepository userRepository, 
            IMapper mapper)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _mapper = mapper;

            _response = new ApiResponseDTO();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CustomRole>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponseDTO>> GetRoles()
        {
            try
            {
                var roles = await _roleRepository.GetAllAsync();
                _response.Result = roles;
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponseDTO>> CreateRole([FromBody] RoleCreateRequestDTO request)
        {
            try
            {
                if (request is null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var roleFromDatabase = await _roleRepository.GetAsync(
                    x => x.Value == request.Value
                        || x.ClearanceLevel == request.ClearanceLevel);

                if (roleFromDatabase is not null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("CustomRole already exists");
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }


                var newRole = _mapper.Map<CustomRole>(request);
                await _roleRepository.CreateAsync(newRole);
                _response.Result = request;
                _response.StatusCode = HttpStatusCode.Created;
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessages.Add(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, _response);
            }
        }

        [HttpPut("{id:long}", Name = "UpdateRole")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiResponseDTO>> UpdateRole(long id, [FromBody] RoleCreateRequestDTO request)
        {
            try
            {
                var roleFromDatabase = _roleRepository.GetAsync(x => x.ID == id);

                if (request is null
                    || roleFromDatabase is null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var model = _mapper.Map<CustomRole>(request);
                model.ID = id;

                await _roleRepository.UpdateRole(model);
                _response.StatusCode = HttpStatusCode.NoContent;
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

        [HttpGet("{id:long}", Name = "GetUsersRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponseDTO>> SetUsersRole(long id, [FromBody] RoleUpdateOnUserRequestDTO request)
        {
            try
            {
                if (id == 0
                    || request is null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;

                    return BadRequest(_response);
                }

                var updatedUser = await _userRepository.UpdateRole(id, request.Value);

                _response.Result = _mapper.Map<UserPublicResponseDTO>(updatedUser);
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
