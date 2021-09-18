using Contact.Models;
using Contact.Models.DomainModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Logic
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<Models.DomainModels.UserContact> _userManager;
        private readonly ILogger<TokenGenerator> _logger;

        public TokenGenerator(ILogger<TokenGenerator> logger, IConfiguration configuration, UserManager<Models.DomainModels.UserContact> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
        }
        public async Task<string> GenerateTokenAsync(Models.DomainModels.UserContact user)
        {
            List<Claim> userClaims = GetClaims(user);
            await AddUserRoles(user, userClaims);
            SymmetricSecurityKey sigingKey = GenerateSigingKey();
            JwtSecurityToken jwtToken = GenerateToken(userClaims, sigingKey);
            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            _logger.LogInformation("Suecessfully Generated Token");
            return token;
        }

        #region Utility Private Methods
        private JwtSecurityToken GenerateToken(List<Claim> authClaims, SymmetricSecurityKey sigingKey)
        {
            return new JwtSecurityToken(
                audience: _configuration["JwtSettings:Audience"],
                issuer: _configuration["JwtSettings:Issuer"],
                claims: authClaims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: new SigningCredentials(sigingKey, SecurityAlgorithms.HmacSha256)
            );
        }
        private SymmetricSecurityKey GenerateSigingKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
        }

        private async Task AddUserRoles(Models.DomainModels.UserContact user, List<Claim> userClaims)
        {
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role));
            }
        }

        private static List<Claim> GetClaims(Models.DomainModels.UserContact user)
        {
            return new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, $"{user.LastName}, {user.FirstName}")
            };
        }
        #endregion
    }
}
