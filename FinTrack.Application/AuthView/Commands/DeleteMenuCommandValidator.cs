using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthView.Commands
{
    public class DeleteMenuCommandValidator : AbstractValidator<DeleteMenuCommand>
    {
        public DeleteMenuCommandValidator() 
        {
            RuleFor(s => s.Id).NotNull().WithMessage("Id can not be null.").NotEmpty().WithMessage("Id is required.");
        }
    }
}
