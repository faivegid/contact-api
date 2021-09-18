using AutoMapper;
using Contact.Logic;
using Contact.Models.DomainModels;
using Contact.Models.DTOs;
using Contact.Repository.Implementaions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<UserContact> _userManager;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountController> _logger;


        public AccountController(
            UserManager<UserContact> userManager,
            ITokenGenerator tokenGenerator,
            ILogger<AccountController> logger,
            IUserRepository userRepository,
            IMapper mapper)
        {
            _userManager = userManager;
            _tokenGenerator = tokenGenerator;
            _logger = logger;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginRequest)
        {
            _logger.LogInformation($"Login Request by {loginRequest.Email}");
            var user = _userRepository.GetUser(u => u.Email == loginRequest.Email);
            if (user == null)
            {
                return NotFound("Resource not Found");
            }
            var result = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (!result)
            {
                return BadRequest("Old password is incorrect");
            }
            var userDTO = _mapper.Map<UserContact, UserDTO>(user);
            var token = await _tokenGenerator.CreateTokenAsync(user);
            HttpContext.Session.SetString("Token", token);
            userDTO.Token = token;
            return Ok(userDTO);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            _logger.LogInformation($"Registration Attempt by {registerDTO.Email}");
            UserContact user = _mapper.Map<RegisterDTO, UserContact>(registerDTO);
            var result = await _userRepository.InsertAsync(user);
            if (result)
            {
                _logger.LogInformation($"Sucessfully register new user for {registerDTO.Email}");
                await _userManager.AddToRoleAsync(user, "Customer");
            }
            return Created("user/getuser", _mapper.Map<UserDTO>(user));
        }
    }
}
