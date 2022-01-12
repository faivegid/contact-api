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
        private readonly UserManager<UserContact> _userManager;

        public TokenGenerator(IConfiguration configuration, UserManager<Models.DomainModels.UserContact> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        public async Task<string> CreateTokenAsync(UserContact user)
        {
            List<Claim> userClaims = await GetClaimsAsync(user);
            SymmetricSecurityKey sigingKey = GenerateSigingKey();
            JwtSecurityToken jwtToken = GenerateToken(userClaims, sigingKey);
            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
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

        private async Task AddUserRoles(UserContact user, List<Claim> userClaims)
        {
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                userClaims.Add(new Claim(ClaimTypes.Role, role));
            }
        }

        private async Task<List<Claim>> GetClaimsAsync(UserContact user)
        {
            var userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, $"{user.LastName}, {user.FirstName}")
            };
            await AddUserRoles(user, userClaims);
            return userClaims;
        }
        #endregion
    }
}
