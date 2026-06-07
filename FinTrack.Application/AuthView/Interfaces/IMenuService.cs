using FinTrack.Application.AuthView.Models;
using FinTrack.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthView.Interfaces
{
    public  interface IMenuService : IDisposable
    {
        Task<Result> CreateMenu(MenuVM menu);
        Task<Result> DeleteMenu(long id);
        Task<IList<MenuItemVM>> GetMenu(string searchBy, int menuLevel);
        Task<MenuItemVM> GetMenuById(long id);
        Task<Result> CreateMenuToGroupPrivilege(MenuToGroupPrivilegeVM menuToGroupPrivilege);
        Task<Result> CreateMenuToUserPrivilege(MenuToUserPrivilegeVM menuToUserPrivilege);
    }
}
