using FinTrack.Application.AuthView.Interfaces;
using FinTrack.Application.AuthView.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthView.Queries
{
    public class GetMenuByIdQueryHandler : IRequestHandler<GetMenuByIdQuery, MenuItemVM>
    {
        private readonly IMenuService _menuService;
        public GetMenuByIdQueryHandler(IMenuService menuService)
        {
            _menuService = menuService;
        }
        public async Task<MenuItemVM> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _menuService.GetMenuById(request.Id);
            return data;
        }
    }
}
