using FinTrack.Application.AuthRole.Interfaces;
using FinTrack.Application.AuthRole.Queries;
using FinTrack.Application.AuthRole.ViewModels;
using FinTrack.Application.Requests.Jamuna.Menus_Url.Interfaces;
using FinTrack.Application.Requests.Jamuna.Menus_Url.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.Menus_Url.Queries
{
    public class GetMenusUrlByIdQueryHandler : IRequestHandler<GetMenusUrlByIdQuery, MenusUrlVM>
    {
        private readonly IMenusUrlService _menusUrlService;
        public GetMenusUrlByIdQueryHandler(IMenusUrlService menusUrlService)
        {
            _menusUrlService = menusUrlService ?? throw new ArgumentNullException(nameof(_menusUrlService));
        }
        public async Task<MenusUrlVM> Handle(GetMenusUrlByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _menusUrlService.GetMenusUrlById(request.Id);
            return data;
        }
    }
}
