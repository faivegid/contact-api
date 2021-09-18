using Contact.Models.Enum;

namespace Contact.Models.DTOs
{
    public class UserDTO : UpdateUserDTO
    {
        public string Id { get; set; }
        public string Token { get; set; }
    }
}
