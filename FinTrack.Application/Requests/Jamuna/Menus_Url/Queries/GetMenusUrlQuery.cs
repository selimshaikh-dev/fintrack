using FinTrack.Application.AuthRole.ViewModels;
using FinTrack.Application.Requests.Jamuna.Menus_Url.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.Menus_Url.Queries
{
    public class GetMenusUrlQuery : IRequest<IList<MenusUrlVM>>
    {
        public GetMenusUrlQuery() { }
    }
}
