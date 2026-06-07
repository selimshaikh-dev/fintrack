using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthRole.Queries
{
    public class GetRoleByIdQueryValidator : AbstractValidator<GetRoleByIdQuery>
    {
        public GetRoleByIdQueryValidator() 
        {
            RuleFor(s => s.Id)
               .NotNull().WithMessage("Role Id can not be null.")
               .NotEmpty().WithMessage("Role Id is required");
        }
    }
}
