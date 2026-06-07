using FinTrack.Application.AuthRole.Interfaces;
using FinTrack.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthRole.Commands
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, ResultModel>
    {
        private readonly IRoleService _roleService;
        public DeleteRoleCommandHandler(IRoleService roleService) 
        {
            _roleService = roleService;
        }
        public async Task<ResultModel> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _roleService.DeleteRole(request.Id);
            return result;
        }
    }
}
