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
    public class GetMenuQueryHandler : IRequestHandler<GetMenuQuery, IList<MenuItemVM>>
    {
        private readonly IMenuService _menuService;
        public GetMenuQueryHandler(IMenuService menuService)
        {
                _menuService = menuService;
        }
        public async Task<IList<MenuItemVM>> Handle(GetMenuQuery request, CancellationToken cancellationToken)
        {
            var data = await _menuService.GetMenu(request.Name, request.MenuLevel);
            return data;
        }
    }
}
