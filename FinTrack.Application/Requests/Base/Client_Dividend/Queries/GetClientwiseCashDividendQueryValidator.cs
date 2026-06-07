using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.Client_Dividend.Queries
{
    public class GetClientwiseCashDividendQueryValidator : AbstractValidator<GetClientwiseCashDividendQuery>
    {
        public GetClientwiseCashDividendQueryValidator() 
        {
            RuleFor(p => p.ClientCode)
            .NotNull().WithMessage("Client Code can not be null.")
            .NotEmpty().WithMessage("Client Code is required.");
            RuleFor(p => p.InstrumentId)
            .NotNull().WithMessage("Instrument ID can not be null.")
            .NotEmpty().WithMessage("Instrument ID is required.");
        }
    }
}