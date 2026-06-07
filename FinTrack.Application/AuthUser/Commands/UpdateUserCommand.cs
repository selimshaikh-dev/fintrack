using FinTrack.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthUser.Commands
{
    public class UpdateUserCommand : IRequest<ResultModel>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string NationalId { get; set; }
        public string PassportNumber { get; set; }
        public string RoleId { get; set; }

        public UpdateUserCommand(string id, string email, string fullname, string contactNumber, string nationalId, string passportNumber, string roleId)
        {
            Id = id;
            Email = email;
            Name = fullname;
            ContactNumber = contactNumber;
            NationalId = nationalId;
            PassportNumber = passportNumber;
            RoleId = roleId;
        }
    }
}
