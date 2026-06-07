using FluentValidation;
using FinTrack.Application.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthRole.Commands
{
    public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
    {
        public UpdateRoleCommandValidator()
        {
            RuleFor(s => s.Id)
               .NotNull().WithMessage("Role ID can not be null.")
               .NotEmpty().WithMessage("Role ID is required");
            RuleFor(s => s.Name)
               .NotNull().WithMessage("Role Name can not be null.")
               .NotEmpty().WithMessage("Role Name is required")
               .Matches(@"^[A-Za-z]+[A-Za-z ]*$").WithMessage("Role name contains only charecter.");
            RuleFor(s => s.ShownAs)
               .NotNull().WithMessage("Role Display Name can not be null.")
               .NotEmpty().WithMessage("Role Display Name is required")
               .Matches(@"^[A-Za-z]+[A-Za-z ]*$").WithMessage("Role name contains only charecter.");
        }
    }
}
