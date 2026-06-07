using FinTrack.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Auth.Commands
{
    public class EmailConfirmationCommand : IRequest<ResultModel>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public EmailConfirmationCommand(string id, string email)
        {
            Id = id;
            Email = email;
        }
    }
}
