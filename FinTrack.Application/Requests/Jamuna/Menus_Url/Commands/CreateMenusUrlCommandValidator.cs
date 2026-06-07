using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.Menus_Url.Commands
{
    public class CreateMenusUrlCommandValidator : AbstractValidator<CreateMenusUrlCommand>
    {
        public CreateMenusUrlCommandValidator()
        {
            RuleFor(s => s.Name)
               .NotNull().WithMessage("Menu's Url Name can not be null.")
               .NotEmpty().WithMessage("Menu's Url Name is required");
        }
    }
}
