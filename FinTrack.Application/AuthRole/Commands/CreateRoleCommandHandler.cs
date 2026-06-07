using AutoMapper;
using FinTrack.Application.Auth.ViewModels;
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
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, ResultModel>
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        public CreateRoleCommandHandler(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService ?? throw new ArgumentNullException(nameof(_roleService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }
        public async Task<ResultModel> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = _mapper.Map<RoleVM>(request);
            var result = await _roleService.CreateRole(role);
            return result;
        }
    }
}
