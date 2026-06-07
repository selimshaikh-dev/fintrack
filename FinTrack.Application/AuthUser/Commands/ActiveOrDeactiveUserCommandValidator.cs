using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthUser.Commands
{
    public class ActiveOrDeactiveUserCommandValidator :AbstractValidator<ActiveOrDeactiveUserCommand>
    {
        public ActiveOrDeactiveUserCommandValidator()
        {
            RuleFor(s => s.Id)
                          .NotNull().WithMessage("Id can not be null.")
                          .NotEmpty().WithMessage("Id is required.");
        }
    }
}
