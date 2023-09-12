using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoppingAssistant.DTOs;
using ShoppingAssistant.DTOs.UserDTOs;
using ShoppingAssistant.Models;
using ShoppingAssistant.Repository.Interfaces;
using ShoppingAssistant.Services.Interfaces;
using System.Net;

namespace ShoppingAssistant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;

        private ApiResponseDTO _response;

        public AuthController(
            IUserRepository userRepository,
            IMapper mapper,
            IPasswordService passwordService, 
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordService = passwordService;
            _tokenService = tokenService;

            _response = new ApiResponseDTO();
        }

        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> Register(UserRegistrationRequestDTO request)
        {
            try
            {
                if (request is null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;

                    return BadRequest(_response);
                }

                var userWithSameEmail = await _userRepository
                    .GetAsync(x => x.Email == request.Email);

                if (userWithSameEmail is not null)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add("User with the same email address already exists");
                    _response.StatusCode = HttpStatusCode.BadRequest;

                    return BadRequest(_response);
                }


                (byte[] passwordHash, byte[] passwordSalt) = await _passwordService.CreatePasswordHash(request.Password);

                var user = _mapper.Map<User>(request);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                await _userRepository.CreateAsync(user);

                _response.Result = _mapper.Map<UserPublicResponseDTO>(user);
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

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<string>> Login(UserLoginRequestDTO request)
        {
            if (request is null)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;

                return BadRequest(_response);
            }

            var userWithSameEmail = await _userRepository
                .GetAsync(x => x.Email == request.Email);

            if (userWithSameEmail is null)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("User with provided email does not exist");
                _response.StatusCode = HttpStatusCode.BadRequest;

                return BadRequest(_response);
            }

            var isPasswordValid = await _passwordService.VerifyPasswordHash(
                request.Password,
                userWithSameEmail.PasswordHash,
                userWithSameEmail.PasswordSalt);

            if (!isPasswordValid)
            {
                return BadRequest("Invalid password!");
            }

            var token = _tokenService.CreateToken(userWithSameEmail);

            return Ok(token);
        }
    }
}
