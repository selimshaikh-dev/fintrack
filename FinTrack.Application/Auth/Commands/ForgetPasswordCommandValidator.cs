using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Auth.Commands
{
    public class ForgetPasswordCommandValidator : AbstractValidator<ForgetPasswordCommand>
    {
        [Obsolete]
        public ForgetPasswordCommandValidator() 
        {
            RuleFor(s => s.Email)
                          .NotNull().WithMessage("Email can not be null.")
                          .NotEmpty().WithMessage("Email is required")
                          .EmailAddress(EmailValidationMode.Net4xRegex).WithMessage("A valid email is required");
        }
    }
}
