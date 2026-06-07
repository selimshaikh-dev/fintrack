using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Auth.Commands
{
    public class LoginCommand : IRequest<object>
    {
        public string Email { get; set; }
        public string Password { get; set; }


        public LoginCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
