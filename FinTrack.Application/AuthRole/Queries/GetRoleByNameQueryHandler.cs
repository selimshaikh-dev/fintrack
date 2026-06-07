using FinTrack.Application.AuthRole.Interfaces;
using FinTrack.Application.AuthRole.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthRole.Queries
{
    public class GetRoleByNameQueryHandler : IRequestHandler<GetRoleByNameQuery, RoleVM>
    {
        private readonly IRoleService _roleService;
        public GetRoleByNameQueryHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public async Task<RoleVM> Handle(GetRoleByNameQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleService.GetRoleByName(request.Name);
            return role;
        }
    }
}
