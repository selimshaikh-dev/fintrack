using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Auth.Commands
{
    public class LoginCommandValidator: AbstractValidator<LoginCommand>
    {
        [Obsolete]
        public LoginCommandValidator()
        {
            RuleFor(s => s.Email)
                          .NotNull().WithMessage("User name can not be null.")
                          .NotEmpty().WithMessage("User name is required.")
                          .EmailAddress(EmailValidationMode.Net4xRegex).WithMessage("A valid email is required");

            RuleFor(p => p.Password).NotEmpty().WithMessage("Your password cannot be empty")
                        .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                        .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                        .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                        .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                        .Matches(@"\d").WithMessage("Your password must contain one or more digits.")
                        .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("Your password must contain one or more special characters.")
                        .Matches("^[^£# “”]*$").WithMessage("Your password must not contain the following characters £ # “” or spaces.");

        }
    }
}
