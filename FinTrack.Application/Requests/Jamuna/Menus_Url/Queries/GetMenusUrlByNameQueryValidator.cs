using FluentValidation;
using FinTrack.Application.AuthRole.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.Menus_Url.Queries
{
    public class GetMenusUrlByNameQueryValidator : AbstractValidator<GetMenusUrlByNameQuery>
    {
        public GetMenusUrlByNameQueryValidator()
        {
            RuleFor(s => s.Name)
               .NotNull().WithMessage("Menus Url Name can not be null.")
               .NotEmpty().WithMessage("Menus Url Name is required");
        }
    }
}
