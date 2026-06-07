using FinTrack.Application.Requests.Jamuna.Menus_Url.Interfaces;
using FinTrack.Application.Requests.Jamuna.Menus_Url.ViewModels;
using MediatR;

namespace FinTrack.Application.Requests.Jamuna.Menus_Url.Queries
{
    public class GetMenusUrlByNameQueryHanlder : IRequestHandler<GetMenusUrlByNameQuery, MenusUrlVM>
    {
        private readonly IMenusUrlService _menusUrlService;
        public GetMenusUrlByNameQueryHanlder(IMenusUrlService menusUrlService)
        {
            _menusUrlService = menusUrlService ?? throw new ArgumentNullException(nameof(_menusUrlService));
        }
        public async Task<MenusUrlVM> Handle(GetMenusUrlByNameQuery request, CancellationToken cancellationToken)
        {
            var data = await _menusUrlService.GetMenusUrlByName(request.Name);
            return data;
        }
    }
}
