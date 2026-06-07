using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.Client_Jamuna.Queries
{
    public class GetClientInfosByEmailQueryValidator: AbstractValidator<GetClientInfosByEmailQuery>
    {
        [Obsolete]
        public GetClientInfosByEmailQueryValidator()
        {
            RuleFor(s => s.Email)
                          .NotNull().WithMessage("User name can not be null.")
                          .NotEmpty().WithMessage("User name is required.")
                          .EmailAddress(EmailValidationMode.Net4xRegex).WithMessage("A valid email is required");
        }
    }
}
