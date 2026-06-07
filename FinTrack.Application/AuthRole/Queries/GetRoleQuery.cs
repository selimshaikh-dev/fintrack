using FinTrack.Application.AuthRole.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthRole.Queries
{
    public class GetRoleQuery : IRequest<IList<RoleVM>>
    {
        public GetRoleQuery() { }
    }
}
