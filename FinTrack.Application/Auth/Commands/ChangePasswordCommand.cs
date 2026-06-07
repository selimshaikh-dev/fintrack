using FinTrack.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Auth.Commands
{
    public class ChangePasswordCommand : IRequest<Result>
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public ChangePasswordCommand(string oldPassword, string newPassword)
        {

            OldPassword = oldPassword;
            NewPassword = newPassword;

        }
    }
}
