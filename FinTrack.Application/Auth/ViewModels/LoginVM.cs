using FinTrack.Application.Auth.Commands;
using FinTrack.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Auth.ViewModels
{
    public class LoginVM : IMapFrom<LoginCommand>
    {
        public string Email { get; set; } 
        public string Password { get; set; }
    }
}
