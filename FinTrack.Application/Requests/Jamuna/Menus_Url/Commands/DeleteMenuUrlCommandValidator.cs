using FluentValidation;
using FinTrack.Application.AuthRole.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.Menus_Url.Commands
{
    public class DeleteMenuUrlCommandValidator : AbstractValidator<DeleteMenuUrlCommand>
    {
        public DeleteMenuUrlCommandValidator()
        {
            RuleFor(s => s.Id)
                        .NotNull().WithMessage("Menus Url Id can not be null.")
                        .NotEmpty().WithMessage("Menus Url Id is required.");
        }
    }
}
