using Contact.Data;
using Contact.DTO;
using Contact.Logic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Contact.API.Controllers
{
    [AllowAnonymous]
    public class AuthCOntroller : Controller
    {
        private readonly ILogger<AppController> _logger;
        private readonly IAuthenticator _authenticator;
        private readonly IUserService _userService;

        public AuthCOntroller(ILogger<AppController> logger,
            IAuthenticator authenticator,
            IUserService userService)
        {
            _logger = logger;
            _authenticator = authenticator;
            _userService = userService;
        }


        [HttpGet("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            try
            {
                UserDTO user = await _authenticator.Login(loginRequest);
                HttpContext.Session.SetString("Token", user.Token);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            try
            {
                UserDTO user = await _authenticator.Register(request);
                return Created(user.Id, user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
