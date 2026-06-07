using FinTrack.Application.AuthRole.ViewModels;
using FinTrack.Application.Common.Models;
using FinTrack.Application.Requests.Jamuna.Menus_Url.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.Menus_Url.Interfaces
{
    public interface IMenusUrlService: IDisposable
    {
        public Task<ResultModel> CreateMenusUrl(MenusUrlVM menusUrlVM);
        public Task<ResultModel> UpdateMenusUrl(MenusUrlVM menusUrlVM);
        public Task<ResultModel> DeleteMenusUrl(long id);
        public Task<IList<MenusUrlVM>> GetMemusUrl();
        public Task<MenusUrlVM> GetMenusUrlById(long id);
        public Task<MenusUrlVM> GetMenusUrlByName(string name);
    }
}
