using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.Client_Dividend.Queries
{
    public class CheckInterimDividendQueryValidator : AbstractValidator<CheckInterimDividendQuery>
    {
        public CheckInterimDividendQueryValidator() 
        {
            RuleFor(p => p.InstrumentId)
            .NotNull().WithMessage("Instrument ID can not be null.")
            .NotEmpty().WithMessage("Instrument ID is required.");
        }
    }
}