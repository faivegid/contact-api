using AutoMapper;
using Contact.Data;
using Contact.DTO;
using Contact.DTO.AutoMapper;
using Contact.Models;
using Contact.Models.DomainModels;
using Contact.Models.DTOs;
using Contact.Models.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Logic
{
    public class Authenticator : IAuthenticator
    {
        private readonly UserManager<Models.DomainModels.UserContact> _userManager;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly ILogger<Authenticator> _logger;
        private readonly IMapper _mapper;

        public Authenticator(UserManager<UserContact> userManager, ITokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<UserDTO> Login(LoginDTO loginRequest)
        {
            Models.DomainModels.UserContact user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, loginRequest.Password))
                {
                    var userDTO = _mapper.Map<UserContact, UserDTO>(user);
                    userDTO.Token = await _tokenGenerator.CreateTokenAsync(user);
                    return userDTO;
                }
            }
            throw new AccessViolationException("Invalid credentials");
        }

   
        public async Task<UserDTO> Register(RegisterDTO request)
        {
            UserContact user = _mapper.Map<UserContact>(request);
            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                return _mapper.Map<UserContact, UserDTO>(user);
            }
            string message = string.Join("\n", result.Errors.Select(x => x.Description));            
            throw new MissingMemberException(message);
        }

    }
}
