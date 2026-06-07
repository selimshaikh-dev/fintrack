using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Consolidate.Portfolio.Queries
{
    public class GetCosolidatedPortfolioReportQueryValidator : AbstractValidator<GetCosolidatedPortfolioReportQuery>
    {
        public GetCosolidatedPortfolioReportQueryValidator()
        {
            RuleFor(s => s.MemberID)
                         .NotNull().WithMessage("Member ID can not be null.")
                         .NotEmpty().WithMessage("Member ID is required.");
            RuleFor(s => s.EndDate)
                         .NotNull().WithMessage("Date can not be null.")
                         .NotEmpty().WithMessage("Date is required.");
        }
    }
}
