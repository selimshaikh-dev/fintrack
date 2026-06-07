using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Auth.Commands
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(p => p.OldPassword).NotEmpty().WithMessage("Old Password cannot be empty")
                        .MinimumLength(8).WithMessage("Old Password length must be at least 8.")
                        .MaximumLength(16).WithMessage("Old Password length must not exceed 16.")
                        .Matches(@"[A-Z]+").WithMessage("Old Password must contain at least one uppercase letter.")
                        .Matches(@"[a-z]+").WithMessage("Old Password must contain at least one lowercase letter.")
                        .Matches(@"\d").WithMessage("Old Password must contain one or more digits.")
                        .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("Old Password must contain one or more special characters.")
                        .Matches("^[^£# “”]*$").WithMessage("Old Password must not contain the following characters £ # “” or spaces.");

            RuleFor(p => p.NewPassword).NotEmpty().WithMessage("New Password cannot be empty")
                        .MinimumLength(8).WithMessage("New Password length must be at least 8.")
                        .MaximumLength(16).WithMessage("New Password length must not exceed 16.")
                        .Matches(@"[A-Z]+").WithMessage("New Password must contain at least one uppercase letter.")
                        .Matches(@"[a-z]+").WithMessage("New Password must contain at least one lowercase letter.")
                        .Matches(@"\d").WithMessage("New Password must contain one or more digits.")
                        .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("New Password must contain one or more special characters.")
                        .Matches("^[^£# “”]*$").WithMessage("New Password must not contain the following characters £ # “” or spaces.");
            RuleFor(u => u)
                          .Must(u => u.OldPassword != u.NewPassword)
                          .WithMessage("New password are not allow to same as old password.");
        }
    }
}
