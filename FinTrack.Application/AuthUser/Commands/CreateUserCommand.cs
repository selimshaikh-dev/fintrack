using FinTrack.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthUser.Commands
{
    public class CreateUserCommand : IRequest<ResultModel>
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string NationalId { get; set; }
        public string PassportNumber { get; set; }
        public string RoleId { get; set; }

        public CreateUserCommand(string id, string email, string name, string contactNumber, string nationalId, string passportnum, string roleId)
        {
            Id = id;    
            Email = email;
            Name = name;
            ContactNumber = contactNumber;   
            NationalId = nationalId;
            PassportNumber = passportnum;                
            RoleId = roleId;               
        }
    }
}
