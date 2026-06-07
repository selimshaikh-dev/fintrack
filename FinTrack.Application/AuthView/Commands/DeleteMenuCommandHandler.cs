using AutoMapper;
using FinTrack.Application.AuthView.Interfaces;
using FinTrack.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthView.Commands
{
    public class DeleteMenuCommandHandler : IRequestHandler<DeleteMenuCommand, Result>
    {
        private readonly IMenuService _menuService;
        private readonly IMapper _mapper;
        public DeleteMenuCommandHandler(IMenuService menuService, IMapper mapper)
        {
            _menuService = menuService ?? throw new ArgumentNullException(nameof(_menuService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }
        public async Task<Result> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
        {
            var result =await _menuService.DeleteMenu(request.Id);
            return result;
        }
    }
}
