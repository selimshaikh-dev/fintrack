using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Commands.Base.ClientCashDividend
{
    public class AddCashDividendCommandValidator : AbstractValidator<AddCashDividendCommand>
    {
        public AddCashDividendCommandValidator()
        {
            RuleFor(s => s.Instrument_ID)
                .GreaterThan(0).WithMessage("Instrument is required.");

            RuleFor(s => s.NetAmount)
                .GreaterThan(0).WithMessage("Net amount must be greater than zero.");

            RuleFor(s => s.GrossAmount)
                .GreaterThan(0).WithMessage("Gross amount must be greater than zero.");

            RuleFor(s => s.TaxAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Tax amount is invalid.");

            RuleFor(s => s.TransactionDate)
                .NotEmpty().WithMessage("Transaction date is required.");

            RuleFor(s => s.MatureDate)
                .NotEmpty().WithMessage("Mature date is required.");

            RuleFor(s => s.Remarks)
                .MaximumLength(500).WithMessage("Remarks is too long.");
        }
    }
}