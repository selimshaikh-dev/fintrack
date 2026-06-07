using FluentValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.Client_Ledger_Details.Queries
{
    public class GetClientLedgerDetailsQueryValidator : AbstractValidator<GetClientLedgerDetailsQuery>
    {
        public GetClientLedgerDetailsQueryValidator()
        {
            RuleFor(p => p.MemberID)
              .NotNull().WithMessage("Member ID can not be null.")
              .NotEmpty().WithMessage("Member ID is required.");
            RuleFor(p => p.StartDate)
              .NotNull().WithMessage("Start Date can not be null.")
              .NotEmpty().WithMessage("Start Date is required.");
            RuleFor(p => p.EndDate)
              .NotNull().WithMessage("End Date can not be null.")
              .NotEmpty().WithMessage("End Date is required.");
        }
    }
}
