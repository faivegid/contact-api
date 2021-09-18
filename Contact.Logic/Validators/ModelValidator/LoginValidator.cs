using Contact.DTO;
using Contact.Models.DTOs;
using FluentValidation;

namespace Contact.Logic.Validators.ModelValidator
{
    public class LoginValidator : AbstractValidator<LoginDTO>
    {
        public LoginValidator()
        {
            RuleFor(login => login.Email).NotEmpty().WithMessage("Can't Login Without and email")
                                         .EmailAddress().WithMessage("Invalid Email format");
            RuleFor(login => login.Password).NotEmpty().WithMessage("Can't login without a password");
        }
    }
}
