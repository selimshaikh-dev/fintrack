using FinTrack.Application.AuthUser.Commands;
using FinTrack.Application.Common.Mappings;
using FinTrack.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthUser.ViewModels
{
    public class UserVM : IMapFrom<CreateUserCommand>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string NationalId { get; set; }
        public string PassportNumber { get; set; }
        public string RoleId { get; set; }

    }
    public class UpdateUserVM : IMapFrom<UpdateUserCommand>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string NationalId { get; set; }
        public string PassportNumber { get; set; }
        public string RoleId { get; set; }

    }
}
