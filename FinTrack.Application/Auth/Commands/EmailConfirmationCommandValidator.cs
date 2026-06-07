using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Auth.Commands
{
    public class EmailConfirmationCommandValidator : AbstractValidator<EmailConfirmationCommand>
    {
        public EmailConfirmationCommandValidator()
        {
            RuleFor(s => s.Id)
                          .NotNull().WithMessage("User id can not be null.")
                          .NotEmpty().WithMessage("User id is required.");
            RuleFor(p => p.Email)
                          .NotNull().WithMessage("Your email can not be null.")
                          .NotEmpty().WithMessage("Your email is required.");                  
        }

    }
}
