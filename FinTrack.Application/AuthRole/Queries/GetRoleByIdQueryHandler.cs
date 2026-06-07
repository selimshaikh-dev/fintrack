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
    public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, RoleVM>
    {
        private readonly IRoleService _roleService;
        public GetRoleByIdQueryHandler(IRoleService roleService)
        {
            _roleService = roleService ?? throw new ArgumentNullException(nameof(_roleService));
        }
        public async Task<RoleVM> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _roleService.GetRoleById(request.Id);
            return role;
        }
    }
}
