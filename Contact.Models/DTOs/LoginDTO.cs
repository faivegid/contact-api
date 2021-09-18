using System.ComponentModel.DataAnnotations;

namespace Contact.Models.DTOs
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
