using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthRole.Queries
{
    public class GetRoleByNameQueryValidator : AbstractValidator<GetRoleByNameQuery>
    {
        public GetRoleByNameQueryValidator()
        {
            RuleFor(s => s.Name)
               .NotNull().WithMessage("Role Name can not be null.")
               .NotEmpty().WithMessage("Role Name is required");
        }
    }
}
