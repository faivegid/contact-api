using Contact.Models.Enum;

namespace Contact.DTO
{
    public class UserDTO : UpdateUserDTO
    {
        public string Id { get; set; }
        public Gender Gender { get; set; }
        public string Token { get; set; }
    }
}
