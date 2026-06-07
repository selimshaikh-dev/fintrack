using AutoMapper;
using FinTrack.Application.AuthRole.Interfaces;
using FinTrack.Application.AuthRole.ViewModels;
using FinTrack.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthRole.Commands
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, ResultModel>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        public UpdateRoleCommandHandler(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService ?? throw new ArgumentNullException(nameof(_roleService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }
        public async Task<ResultModel> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = _mapper.Map<RoleUpdateVM>(request);
            var result = await _roleService.UpdateRole(role);
            return result;
        }
    }
}
