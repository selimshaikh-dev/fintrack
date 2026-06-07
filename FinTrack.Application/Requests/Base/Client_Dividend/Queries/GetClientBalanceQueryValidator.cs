using FluentValidation;
using FinTrack.Application.Requests.Jamuna.Client_Ledger_Details.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.Client_Dividend.Queries
{
    public class GetClientBalanceQueryValidator : AbstractValidator<GetClientBalanceQuery>
    {

        public GetClientBalanceQueryValidator() 
        {
            RuleFor(p => p.ClientCode)
           .NotNull().WithMessage("Client Code can not be null.")
           .NotEmpty().WithMessage("Client Code is required.");
        }
    }
}
