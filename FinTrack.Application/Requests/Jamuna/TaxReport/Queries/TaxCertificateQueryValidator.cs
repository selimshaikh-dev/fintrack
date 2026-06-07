using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.TaxReport.Queries
{
    public class TaxCertificateQueryValidator : AbstractValidator<TaxCertificateQuery>
    {
        public TaxCertificateQueryValidator()
        {
            RuleFor(p => p.MemberID)
              .NotNull().WithMessage("Member ID can not be null.")
              .NotEmpty().WithMessage("Member ID Code is required.");
            RuleFor(p => p.FinancialYear)
              .NotNull().WithMessage("Financial Year can not be null.")
              .NotEmpty().WithMessage("Financial Year is required.");
        }
    }
}
