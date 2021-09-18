using Contact.DTO;
using Contact.Models.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Logic.Validators.ModelValidator
{
    public class RegisterVAlidator : AbstractValidator<RegisterDTO>
    {
        public RegisterVAlidator()
        {
            RuleFor(request => request.Email).NotEmpty().EmailAddress();
            RuleFor(request => request.FirstName).NotEmpty();
            RuleFor(request => request.LastName).NotEmpty();
            RuleFor(request => request.Password).NotEmpty();
            RuleFor(request => request.ConfirmPassword).NotEmpty().Equal(request => request.Password).WithMessage("Password does not Match");
        }
    }
}
