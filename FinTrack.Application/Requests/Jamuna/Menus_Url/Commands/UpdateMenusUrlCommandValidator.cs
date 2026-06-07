using FluentValidation;
using FinTrack.Application.AuthRole.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.Menus_Url.Commands
{
    public class UpdateMenusUrlCommandValidator : AbstractValidator<UpdateMenusUrlCommand>
    {
        public UpdateMenusUrlCommandValidator()
        {
            RuleFor(s => s.Id)
               .NotNull().WithMessage("Menus Url ID can not be null.")
               .NotEmpty().WithMessage("Menus Url is required");
            RuleFor(s => s.Name)
               .NotNull().WithMessage("Menus Url Name can not be null.")
               .NotEmpty().WithMessage("Menus Url Name is required");
        }
    }
}
