using AutoMapper;
using FinTrack.Application.AuthView.Interfaces;
using FinTrack.Application.AuthView.Models;
using FinTrack.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthView.Commands
{
    public class CreateMenuToGroupPrivilegeCommandHandler : IRequestHandler<CreateMenuToGroupPrivilegeCommand, Result>
    {
        private readonly IMenuService _menuService;
        private readonly IMapper _mapper;
        public CreateMenuToGroupPrivilegeCommandHandler(IMenuService menuService, IMapper mapper)
        {
            _menuService = menuService ?? throw new ArgumentNullException(nameof(_menuService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }
        public Task<Result> Handle(CreateMenuToGroupPrivilegeCommand request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<MenuToGroupPrivilegeVM>(request);
            var result = _menuService.CreateMenuToGroupPrivilege(data);
            return result;
        }
    }
}
