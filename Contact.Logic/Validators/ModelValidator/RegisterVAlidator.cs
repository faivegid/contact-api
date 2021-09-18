using Contact.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Logic.Validators.ModelValidator
{
    public class RegisterVAlidator : AbstractValidator<RegisterRequest>
    {
        public RegisterVAlidator()
        {
            RuleFor(request => request.Email).NotEmpty().EmailAddress();
            RuleFor(request => request.FirstName).NotEmpty().Matches(@"^[a-bA-Z]$");
            RuleFor(request => request.LastName).NotEmpty().Matches(@"^[a-bA-Z]$");
            RuleFor(request => request.Password).NotEmpty();
            RuleFor(request => request.ConfirmPassword).NotEmpty().Equal(request => request.Password).WithMessage("Password does not Match");
            RuleFor(request => request.Gender).NotEmpty();
        }
    }
}
