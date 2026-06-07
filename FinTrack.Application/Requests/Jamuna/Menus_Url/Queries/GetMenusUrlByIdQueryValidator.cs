using FluentValidation;
using FinTrack.Application.AuthRole.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.Menus_Url.Queries
{
    public class GetMenusUrlByIdQueryValidator : AbstractValidator<GetMenusUrlByIdQuery>
    {
        public GetMenusUrlByIdQueryValidator()
        {
            RuleFor(s => s.Id)
               .NotNull().WithMessage("Menus Url Id can not be null.")
               .NotEmpty().WithMessage("Menus Url Id is required");
        }
    }
}
