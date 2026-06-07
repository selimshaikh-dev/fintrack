using FinTrack.Application.AuthUser.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthUser.Queries
{
    public class GetUserByEmailCommand : IRequest<UserReturnVM>
    {
        public string Email { get; set; }
        public GetUserByEmailCommand(string email)
        {
            Email = email;
        }
    }
}
