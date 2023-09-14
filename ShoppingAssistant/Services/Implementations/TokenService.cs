using Microsoft.IdentityModel.Tokens;
using ShoppingAssistant.Models;
using ShoppingAssistant.Repository.Interfaces;
using ShoppingAssistant.Services.Interfaces;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ShoppingAssistant.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IRoleRepository _roleRepository;

        public TokenService(IConfiguration configuration, IRoleRepository roleRepository)
        {
            _configuration = configuration;
            _roleRepository = roleRepository;
        }

        public string CreateToken(User user)
        {
            var role = _roleRepository.GetAsync(x => x.ID == user.CustomRoleID).Result;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, role.Value)
            };

            var secretToken = _configuration.GetSection("AppSettings:Token").Value;
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secretToken));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
