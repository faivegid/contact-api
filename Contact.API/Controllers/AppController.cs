using Contact.Data;
using Contact.DTO;
using Contact.Logic;
using Contact.Models.DTOs;
using Contact.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.SecurityTokenService;
using System;
using System.Threading.Tasks;

namespace Contact.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AppController : Controller
    {
        private readonly ILogger<AppController> _logger;
        private readonly IAuthenticator _authenticator;
        private readonly IUserService _userService;
        private readonly ITokenGenerator _tokenGenerator;

        public AppController(ILogger<AppController> logger,
            IAuthenticator authenticator,
            IUserService userService, ITokenGenerator tokenGenerator)
        {
            _logger = logger;
            _authenticator = authenticator;
            _userService = userService;
            _tokenGenerator = tokenGenerator;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("getall")]
        private IActionResult Get()
        {
            return Ok();
        }

        [HttpPatch("update")]
        public IActionResult Update(string userId, UpdateUserDTO update)
        {
            try
            {
                _userService.UpdateAsync(userId, update);
                var result = _userService.GetByIdAsync(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("updatepassword")]
        public async Task<IActionResult> UpdatePassWordAsync(string userId, UpdatePasswordDTO updatePassword)
        {
            try
            {
                await _userService.UpdatePassword(userId, updatePassword);
                return Ok(await _userService.GetByIdAsync(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteAsync(string userId)
        {
            try
            {
                bool result = await _userService.DeleteUser(userId);
                if (result)
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
