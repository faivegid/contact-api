using System.ComponentModel.DataAnnotations;

namespace Contact.DTO
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
