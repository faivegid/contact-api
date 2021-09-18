using Contact.Models.Enum;
using Microsoft.AspNetCore.Identity;
using System;

namespace Contact.Models.DomainModels
{
    public class UserContact : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }       
        public string ProfileImage { get; set; }
        public Gender Gender {  get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
