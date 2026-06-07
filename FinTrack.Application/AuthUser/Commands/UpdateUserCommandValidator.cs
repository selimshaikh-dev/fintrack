using FluentValidation;
using FluentValidation.Validators;
using FinTrack.Application.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthUser.Commands
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        [Obsolete]
        public UpdateUserCommandValidator()
        {
            RuleFor(s => s.Id)
                          .NotNull().WithMessage("User Id can not be null.")
                          .NotEmpty().WithMessage("User Id is required.");
            RuleFor(s => s.Email)
                          .NotNull().WithMessage("Email can not be null.")
                          .NotEmpty().WithMessage("Email is required.")
                          .EmailAddress(EmailValidationMode.Net4xRegex).WithMessage("A valid email is required.");
            RuleFor(s => s.Name)
                          .NotNull().WithMessage("Full name can not be null.")
                          .NotEmpty().WithMessage("Full name is required.")
                          .MaximumLength(50).WithMessage("Full name must not exceed 50 letter.")
                          .Matches(@"^[A-Za-z]+[A-Za-z ]*$").WithMessage("Full name must contains only charecter.");
            RuleFor(s => s.ContactNumber)
                          .NotNull().WithMessage("Contact number can not be null.")
                          .NotEmpty().WithMessage("Contact number is required.")
                          .Must(ValidationCheckerHelper.IsValidNumber).WithMessage("Contact number must contains only number.");
            RuleFor(s => s.RoleId)
                          .NotNull().WithMessage("Role Id can not be null.")
                          .NotEmpty().WithMessage("Role Id is required.");

        }
    }
}
