using AutoMapper;
using Contact.Logic.UploadImage;
using Contact.Models.DomainModels;
using Contact.Models.DTOs;
using Contact.Repository.Implementaions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Contact.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IImageUploader _uploader;

        public UserController(
            IUserRepository userRepository,
            ILogger<UserController> logger,
            IMapper mapper,
            IImageUploader uploader)
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
            _uploader = uploader;
        }

        [HttpGet("getpagelist")]
        public async Task<IActionResult> GetContacts([FromQuery] PagingDTO pager)
        {
            var page = await _userRepository.GetPageList(pager);
            return Ok(page);
        }

        [HttpGet("getcontact")]
        public IActionResult GetContact(string email)
        {
            var user = _userRepository.GetUser(user => user.Email == email);
            if(user == null)
            {
                return NotFound("Resource not found");
            }
            return Ok(_mapper.Map<UserDTO>(user));
        }

        [HttpPatch("update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserDTO update)
        {
            var userId = HttpContext.User.FindFirst(user => user.Type == ClaimTypes.NameIdentifier).Value;
            UserContact user = _userRepository.GetUser(user => user.Id == userId);
            user = _mapper.Map(update, user);
            var result = await _userRepository.UpdateAsync(user);
            return NoContent();
        }

        [HttpPatch("changepassword")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordDTO update)
        {
            var userId = HttpContext.User.FindFirst(user => user.Type == ClaimTypes.NameIdentifier).Value;
            UserContact user = _userRepository.GetUser(user => user.Id == userId);
            var result = await _userRepository.UpdatePassword(user, update.OldPassword, update.NewPassword);
            return result ? NoContent() : BadRequest("Invalid Password");
        }

        [HttpPut("uploadimage")]
        public async Task<IActionResult> UpdateImageAsync([FromForm] ImageFileDTO image)
        {
            var result = await _uploader.UploadImageAsync(image.file);
            var userId = HttpContext.User.FindFirst(user => user.Type == ClaimTypes.NameIdentifier).Value;
            UserContact user = _userRepository.GetUser(user => user.Id == userId);
            user.ProfileImage = result.Url.ToString();
            await _userRepository.UpdateAsync(user);
            return Ok(result.Url.ToString());
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete([FromBody] string email)
        {
            UserContact user = _userRepository.GetUser(user => user.Email == email);
            if (user == null)
            {
                return BadRequest();
            }
            var result = await _userRepository.DeleteAsync(user);
            return NoContent();
        }



    }
}
