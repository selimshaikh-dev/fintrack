using FluentValidation;
using FluentValidation.Validators;
using FinTrack.Application.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Auth.Commands
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        [Obsolete]
        public RegisterCommandValidator()
        {
            RuleFor(s => s.Email)
                          .NotNull().WithMessage("Email can not be null.")
                          .NotEmpty().WithMessage("Email is required")
                          .EmailAddress(EmailValidationMode.Net4xRegex).WithMessage("A valid email is required");

            RuleFor(p => p.Password).NotEmpty().WithMessage("Your password cannot be empty")
                          .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                          .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                          .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                          .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                          .Matches(@"\d").WithMessage("Your password must contain one or more digits.")
                          .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("Your password must contain one or more special characters.")
                          .Matches("^[^£# “”]*$").WithMessage("Your password must not contain the following characters £ # “” or spaces.");
            RuleFor(s => s.FullName)
                          .NotNull().WithMessage("Your full name can not be null.")
                          .NotEmpty().WithMessage("Your full name is required.")
                          //.Matches(@"^[a-zA-Z ]*$").WithMessage("Full name should be all letters.")
                          .MaximumLength(50).WithMessage("You full name length must not exceed 50.");
            RuleFor(s => s.PhoneNumber)
                          .NotNull().WithMessage("Your phone number can not be null.")
                          .NotEmpty().WithMessage("Your phone number is required.")
                          .MinimumLength(11).WithMessage("Your phone number must contain at least 11 digit.");
            RuleFor(s => s.DateOfBirth)
                          .NotNull().WithMessage("Your birth date can not be null.")
                          .NotEmpty().WithMessage("Your birth date is required.");
        }
        private bool IsValidName(string name)
        {
            return ValidationCheckerHelper.IsValidName(name);
        }
    }
}
