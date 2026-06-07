using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthView.Commands
{
    public class CreateMenuCommandValidator : AbstractValidator<CreateMenuCommand>
    {
        public CreateMenuCommandValidator()
        {
            RuleFor(s => s.Title)
                          .NotNull().WithMessage("Title can not be null.")
                          .NotEmpty().WithMessage("Title is required.");
            RuleFor(s => s.Type)
                          .NotNull().WithMessage("Type can not be null.")
                          .NotEmpty().WithMessage("Type is required.");
            RuleFor(s => s.IsMenuItem)
                          .NotNull().WithMessage("MenuItem can not be null.")
                          .NotEmpty().WithMessage("MenuItem is required.");
            RuleFor(s => s.Active)
                          .NotNull().WithMessage("Active can not be null.")
                          .NotEmpty().WithMessage("Active is required.");
            RuleFor(s => s.DisplayOrder)
                          .NotNull().WithMessage("Display order can not be null.")
                          .NotEmpty().WithMessage("Display order is required.");
        }
    }
}
