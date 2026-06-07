using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthUser.Commands
{
    public class DeleteUserCommandValidator: AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(s => s.Id)
                          .NotNull().WithMessage("User Id can not be null.")
                          .NotEmpty().WithMessage("User Id is required.");
        }
    }
}
