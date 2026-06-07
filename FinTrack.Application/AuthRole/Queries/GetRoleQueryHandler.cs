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
    public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, IList<RoleVM>>
    {
        private readonly IRoleService _roleService;
        public GetRoleQueryHandler(IRoleService roleService)
        {
            _roleService = roleService ?? throw new ArgumentNullException(nameof(_roleService));
        }
        public async Task<IList<RoleVM>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleService.GetRole();
            return roles;
        }
    }
}
