using Microsoft.AspNetCore.Http;

namespace Contact.API.Controllers
{
    public class ImageFileDTO
    {
        public IFormFile file { get; set; }
    }
}