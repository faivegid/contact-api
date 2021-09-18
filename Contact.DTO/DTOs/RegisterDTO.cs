using Contact.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.DTO
{
    public class RegisterDTO : UpdateUserDTO
    {      
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public Gender Gender { get; set; }
    }
}
