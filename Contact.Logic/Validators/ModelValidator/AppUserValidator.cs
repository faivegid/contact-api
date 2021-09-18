using Contact.Models;
using FluentValidation;

namespace Contact.Logic.Validators.ModelValidator
{
    public class AppUserValidator : AbstractValidator<AppUser>
    {
        public AppUserValidator()
        {
            RuleFor(user => user.FirstName).NotEmpty().Matches(@"^[a-bA-Z]$").WithMessage("Names can only be alphabets").MinimumLength(3);
            RuleFor(user => user.LastName).NotEmpty().Matches(@"^[a-bA-Z]$").WithMessage("Names can only be alphabets").MinimumLength(3);
            RuleFor(user => user.Email).NotEmpty().EmailAddress().WithMessage("Invalid email format");
            RuleFor(user => user.PhoneNumber).NotEmpty().Matches(@"^[0-9]$").WithMessage("Phone Number can only be digits").Length(11).WithMessage("Phone Number can only be 11 digits");
        }
    }
}
