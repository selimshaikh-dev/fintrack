using FinTrack.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Auth.Commands
{
    public class SetPasswordCommand : IRequest<ResultModel>
    {
        public string Id { get; set; }
        public string Password { get; set; }
    }
}
