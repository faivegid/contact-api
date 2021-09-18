using Contact.DTO;
using FluentValidation;

namespace Contact.Logic.Validators.ModelValidator
{
    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(login => login.Email).NotEmpty().WithMessage("Can't Login Without and email")
                                         .EmailAddress().WithMessage("Invalid Email format");
            RuleFor(login => login.Password).NotEmpty().WithMessage("Can't login without a password");
        }
    }
}
