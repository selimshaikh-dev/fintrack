using FinTrack.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Auth.Commands
{
    public class ForgetPasswordCommand : IRequest<ResultModel>
    {
        public string Email { get; set; }
        public ForgetPasswordCommand(string email, string password)
        {
            Email = email;
        }
    }
}
