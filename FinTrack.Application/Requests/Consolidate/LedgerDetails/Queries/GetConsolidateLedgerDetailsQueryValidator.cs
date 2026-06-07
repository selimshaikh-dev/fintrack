using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Consolidate.LedgerDetails.Queries
{
    public class GetConsolidateLedgerDetailsQueryValidator: AbstractValidator<GetConsolidateLedgerDetailsQuery>
    {
        public GetConsolidateLedgerDetailsQueryValidator()
        {
            RuleFor(s => s.MemberID)
                         .NotNull().WithMessage("Member ID can not be null.")
                         .NotEmpty().WithMessage("Member ID is required.");
            RuleFor(s => s.StartDate)
                         .NotNull().WithMessage("Start Date can not be null.")
                         .NotEmpty().WithMessage("Start Date is required.");
            RuleFor(s => s.EndDate)
                         .NotNull().WithMessage("End Date can not be null.")
                         .NotEmpty().WithMessage("End Date is required.");
        }
    }
}
