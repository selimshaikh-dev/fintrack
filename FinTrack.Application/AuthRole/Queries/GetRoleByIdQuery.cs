using FinTrack.Application.AuthRole.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthRole.Queries
{
    public class GetRoleByIdQuery : IRequest<RoleVM>
    {
        public string Id { get; set; }
        public GetRoleByIdQuery(string id) 
        {
            Id = id;
        }
    }
}
