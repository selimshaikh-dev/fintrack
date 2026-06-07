using AutoMapper;
using FinTrack.Application.AuthUser.Interfaces;
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
    public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, Result>
    {
        private readonly IMenuService _menuService;
        private readonly IMapper _mapper;
        public CreateMenuCommandHandler(IMenuService menuService, IMapper mapper)
        {
            _menuService = menuService ?? throw new ArgumentNullException(nameof(_menuService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper)); 
        }

        public async Task<Result> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<MenuVM>(request);
            var result = await _menuService.CreateMenu(data);
            return result;

        }
    }
}
