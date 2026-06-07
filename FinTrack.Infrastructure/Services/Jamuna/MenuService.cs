using FinTrack.Application.AuthView.Interfaces;
using FinTrack.Application.AuthView.Models;
using FinTrack.Application.Common.Interfaces;
using FinTrack.Application.Common.Models;
using FinTrack.Domain.Entities.Auth.AuthViews;
using FinTrack.Domain.Enums;
using FinTrack.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Infrastructure.Services.Jamuna
{
    public class MenuService : SqlDbContext<MenuItemVM>, IMenuService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUser _user;
        public bool result;
        public MenuService(IConfiguration configuration, ApplicationDbContext context,IUser user) : base(configuration)
        {
            _context = context;
            _user = user;
        }

        public async Task<Result> CreateMenu(MenuVM menu)
        {
            try
            {
                if (menu.Id > 0)
                {
                    var result = await Update(menu);
                    return result;
                }
                else
                {
                    var result = await Insert(menu);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return (Result.Failure(new List<string> { ex.ToString() }));
            }
        }
        public async Task<bool> IsExistView(MenuVM model)
        {
            var data = await _context.Menus.Where(x => x.Title == model.Title && x.URL == model.URL && x.Deleted == false).FirstOrDefaultAsync();
            if (data != null)
            {
                return true;
            }
            return false;

        }
        public async Task<Result> Insert(MenuVM model)
        {
            try
            {
                var IsExist = await IsExistView(model);
                if (!IsExist)
                {
                    var entity = new Menu
                    {
                        Active = true,
                        DisplayOrder = model.DisplayOrder,
                        IconClass = model.IconClass,
                        IsMenuItem = model.IsMenuItem,
                        ParentId = model.ParentId,
                        Title = model.Title,
                        Type = model.Type,
                        URL = model.URL,
                        Deleted = false
                    };
                    _context.Menus.Add(entity);
                    await _context.SaveChangesAsync(CancellationToken.None);
                    return (Result.Success("Menu created successfully."));
                }
                else
                {
                    return (Result.Failure(new List<string> { "Menu is not created." }));
                }
            }
            catch (Exception ex)
            {
                return (Result.Failure(new List<string> { ex.ToString() }));
            }
        }
        public async Task<Result> Update(MenuVM model)
        {
            var entity = await _context.Menus.FirstOrDefaultAsync(x => x.Id == model.Id && !x.Deleted);
            if (entity != null)
            {
                entity.DisplayOrder = model.DisplayOrder;
                entity.IconClass = model.IconClass;
                entity.IsMenuItem = model.IsMenuItem;
                entity.ParentId = model.ParentId;
                entity.Title = model.Title;
                entity.Type = model.Type;
                entity.URL = model.URL;
                entity.Active = model.Active;
                await _context.SaveChangesAsync(CancellationToken.None);
                return (Result.Success("Menu updated successfully."));
            }
            else
            {
                return (Result.Failure(new List<string> { "Menu could not be updated." }));
            }
        }

        public async Task<Result> DeleteMenu(long id)
        {
            try
            {
                var model = await _context.Menus.FirstOrDefaultAsync(x => x.Id == id && !x.Deleted);
                if (model == null)
                {
                    return (Result.Failure(new List<string> { "Menu could not found." }));
                }
                else
                {
                    model.Deleted = true;
                    await _context.SaveChangesAsync(CancellationToken.None);
                    return (Result.Success("Menu deleted successfully."));
                }
            }
            catch (Exception ex)
            {
                return (Result.Failure(new List<string> { ex.ToString()}));
            }
        }

        public async Task<IList<MenuItemVM>> GetMenu(string searchBy, int menuLevel)
        {
            var numOfGrandChildren = menuLevel; // 5 maximum grandchildren
            string childInclude = "";
            for (var i = 0; i < numOfGrandChildren; i++)
            {
                if (childInclude != "") childInclude += ".";
                childInclude += "MenusMulti";
            }
            var data = await _context.Menus
                .Include(childInclude)
                .Where(x => !x.Deleted && x.ParentId == null).ToListAsync();

            var list = new List<MenuItemVM>();
            foreach (var item in data.OrderBy(x => x.DisplayOrder))
            {
                list.Add(BindMenus(item, 0, menuLevel));
            }
            return list;
        }
        private MenuItemVM BindMenus(Menu dbMenu, int currentLevel, int maxLevel)
        {
            var mappedData = new MenuItemVM
            {
                Id = dbMenu.Id,
                Title = dbMenu.Title,
                DisplayOrder = dbMenu.DisplayOrder,
                IconClass = dbMenu.IconClass,
                IsMenuItem = dbMenu.IsMenuItem,
                ParentId = dbMenu.ParentId,
                URL = dbMenu.URL,
                Type = dbMenu.Type,
                Active = dbMenu.Active

            };

            if (dbMenu.MenusMulti.Count() > 0 && currentLevel < maxLevel)
            {
                foreach (var item in dbMenu.MenusMulti.Where(x => !x.Deleted).OrderBy(x => x.DisplayOrder))
                {
                    mappedData.Childs.Add(BindMenus(item, currentLevel + 1, maxLevel));
                }
            }
            return mappedData;
        }

        public async Task<MenuItemVM> GetMenuById(long id)
        {
            var data =await _context.Menus.FirstOrDefaultAsync(x=>x.Id == id);
            var mappedData = new MenuItemVM
            {
                Id = data.Id,
                Title = data.Title,
                DisplayOrder = data.DisplayOrder,
                IconClass = data.IconClass,
                IsMenuItem = data.IsMenuItem,
                ParentId = data.ParentId,
                URL = data.URL,
                Type = data.Type,
                Active = data.Active

            };
            return mappedData;
        }

        public async void Dispose()
        {
          await  _context.DisposeAsync();
        }

        public async Task<Result> CreateMenuToGroupPrivilege(MenuToGroupPrivilegeVM menuToGroupPrivilege)
        {
            try
            {
                if (menuToGroupPrivilege.Id == 0)
                {
                    var result = await InsertMenuToGroupPrivilege(menuToGroupPrivilege);
                    return result;
                }
                else
                {
                    var result = await UpdateMenuToGroupPrivilege(menuToGroupPrivilege);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return (Result.Failure(new List<string> { "Menu to Group Privilege could not create or update due to ." + ex.ToString() }));
            }
        }
        public async Task<Result> InsertMenuToGroupPrivilege(MenuToGroupPrivilegeVM menuToGroupPrivilege)
        {
            var entity = new MenusGroupPrivilege
            {
                MenuId = menuToGroupPrivilege.MenuId,
                AuthRoleId = menuToGroupPrivilege.RoleId
            };
            if (await IsExistGroupPrivilege(menuToGroupPrivilege.RoleId, menuToGroupPrivilege.MenuId))
            {
                return (Result.Failure(new List<string> { "Already Exist." }));
            }
            var result = await CheckMenusToGroupPrivilege(menuToGroupPrivilege.RoleId, menuToGroupPrivilege.MenuId);
            if (!result)
            {
                return (Result.Failure(new List<string> { "Please give parent menu's group privilege first." }));
            }
            await _context.MenusGroupPrivileges.AddAsync(entity);
            await _context.SaveChangesAsync();
            return (Result.Success("Menu to group privilege inserted successfully."));

        }
        public async Task<Result> UpdateMenuToGroupPrivilege(MenuToGroupPrivilegeVM menuToGroupPrivilege)
        {

            var entity = await _context.MenusGroupPrivileges.FirstOrDefaultAsync(x => x.Id == menuToGroupPrivilege.Id);
            if (entity == null)
            {
                return (Result.Failure(new List<string> { "No information found." }));
            }
            await _context.SaveChangesAsync();
            return (Result.Success("Menu to group privilege updated successfully."));

        }
        //private MenusGroupPrivilege MappGroupPermission(MenusGroupPrivilege menusGroupPrivilege, int type, bool Checked)
        //{
        //    if (type == 1)
        //    {
        //        menusGroupPrivilege.CanRead = Checked;
        //    }
        //    else if (type == 2)
        //    {
        //        menusGroupPrivilege.CanCreate = Checked;
        //    }
        //    else if (type == 3)
        //    {
        //        menusGroupPrivilege.CanUpdate = Checked;
        //    }
        //    else if (type == 4)
        //    {
        //        menusGroupPrivilege.CanDelete = Checked;
        //    }
        //    else if (type == 5)
        //    {
        //        menusGroupPrivilege.CanRead = Checked;
        //        menusGroupPrivilege.CanCreate = Checked;
        //        menusGroupPrivilege.CanUpdate = Checked;
        //        menusGroupPrivilege.CanDelete = Checked;
        //    }
        //    return menusGroupPrivilege;
        //}


        public async Task<Result> CreateMenuToUserPrivilege(MenuToUserPrivilegeVM menuToUserPrivilege)
        {
            try
            {
                if (menuToUserPrivilege.Id == 0)
                {
                    var result = await InsertMenuToUserPrivilege(menuToUserPrivilege);
                    return result;
                }
                else
                {
                    var result = await UpdateMenuToUserPrivilege(menuToUserPrivilege);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return (Result.Failure(new List<string> { "Menu to User Privilege could not create or update due to ." + ex.ToString() }));
            }
        }
        public async Task<Result> InsertMenuToUserPrivilege(MenuToUserPrivilegeVM menuToUserPrivilege)
        {
            var entity = new MenusUserPrivilege
            {
                MenuId = menuToUserPrivilege.MenuId,
                ApplicationUserId = menuToUserPrivilege.UserId
            };
            var userRole = await _context.UserRoles.FirstOrDefaultAsync(x => x.UserId == menuToUserPrivilege.UserId);
            if (userRole == null)
            {
                return (Result.Failure(new List<string> { "User not found." }));
            }
            if (await IsExistUserPrivilege(userRole.UserId, menuToUserPrivilege.MenuId))
            {
                return (Result.Failure(new List<string> { "Already Exist." }));
            }
             
            if (!await CheckMenusToGroupPrivilege(userRole.RoleId, menuToUserPrivilege.MenuId))
            {
                return (Result.Failure(new List<string> { "Please give parent menu's group privilege first." }));
            }
            if (!await _context.MenusGroupPrivileges.AnyAsync(x => x.AuthRoleId == userRole.RoleId && x.MenuId == menuToUserPrivilege.MenuId))
            {
                return (Result.Failure(new List<string> { "Please give menu's group privilege first." }));
            }

            entity = MappUserPermission(entity, menuToUserPrivilege.Type, menuToUserPrivilege.Checked);
            await _context.MenusUserPrivileges.AddAsync(entity);
            await _context.SaveChangesAsync();
            return (Result.Success("Menu to user privilege inserted successfully."));
        }
        public async Task<Result> UpdateMenuToUserPrivilege(MenuToUserPrivilegeVM menuToUserPrivilege)
        {
            var entity = await _context.MenusUserPrivileges.FirstOrDefaultAsync(x => x.Id == menuToUserPrivilege.Id);
            if (entity == null)
            {
                return (Result.Failure(new List<string> { "No information found." }));
            }
            var userRole = await _context.UserRoles.FirstOrDefaultAsync(x => x.UserId == menuToUserPrivilege.UserId);
            if (userRole == null)
            {
                return (Result.Failure(new List<string> { "User not found." }));
            }
            if (!await CheckMenusToGroupPrivilege(userRole.RoleId, menuToUserPrivilege.MenuId))
            {
                return (Result.Failure(new List<string> { "Please give parent menu's group privilege first." }));
            }
            if (!await _context.MenusGroupPrivileges.AnyAsync(x => x.AuthRoleId == userRole.RoleId && x.MenuId == menuToUserPrivilege.MenuId))
            {
                return (Result.Failure(new List<string> { "Please give menu's group privilege first." }));
            }

            entity = MappUserPermission(entity, menuToUserPrivilege.Type, menuToUserPrivilege.Checked);
            await _context.SaveChangesAsync();
            return (Result.Success("Menu to user privilege updated successfully."));
        }
        private MenusUserPrivilege MappUserPermission(MenusUserPrivilege menusUserPrivilege, int type, bool Checked)
        {
            if (type == 1)
            {
                menusUserPrivilege.CanRead = Checked;
            }
            else if (type == 2)
            {
                menusUserPrivilege.CanCreate = Checked;
            }
            else if (type == 3)
            {
                menusUserPrivilege.CanUpdate = Checked;
            }
            else if (type == 4)
            {
                menusUserPrivilege.CanDelete = Checked;
            }
            else if (type == 5)
            {
                menusUserPrivilege.CanRead = Checked;
                menusUserPrivilege.CanCreate = Checked;
                menusUserPrivilege.CanUpdate = Checked;
                menusUserPrivilege.CanDelete = Checked;
            }
            return menusUserPrivilege;
        }

        public async Task<bool> CheckMenusToGroupPrivilege(string roleId, long? menuId)
        {
            var parentMenuId = await _context.Menus.Include(x => x.MenuSingle).Where(x => x.Id == menuId).Select(x=>x.ParentId).FirstOrDefaultAsync();
            if (parentMenuId == null)
            {
                result = true;
            }
            else
            {
                var groupPrivilege = await _context.MenusGroupPrivileges.FirstOrDefaultAsync(x => x.AuthRoleId == roleId && x.MenuId == parentMenuId);
                if (groupPrivilege == null)
                {
                    result = false;
                }
                else 
                { await CheckMenusToGroupPrivilege(roleId, parentMenuId);
                }               
            }
            return result;
        }
        public async Task<bool> IsExistGroupPrivilege(string roleId, long menuId)
        {
             return  await _context.MenusGroupPrivileges.AnyAsync(x => x.AuthRoleId == roleId && x.MenuId == menuId);
        }
        public async Task<bool> IsExistUserPrivilege(string userId, long menuId)
        {
            var selectedRolesId = new List<string>();
            selectedRolesId.Add("14dd02af-bcb2-4509-b25d-086a517c45f6");
            var authViewAccessList = await _context.MenusUserPrivileges
                .Include(x => x.Menus.MenusUserPrivileges)
                .Include(x => x.Menus)
                .ThenInclude(x => x.MenusMulti)
                .ThenInclude(x => x.MenusUserPrivileges)
                .Where(x => selectedRolesId.Contains(x.ApplicationUserId) &&
                 x.CanRead && x.Menus.Active && x.Menus.IsMenuItem)
                 .OrderBy(x => x.Menus.DisplayOrder)
                 .Select(x => x.Menus)
                 .ToListAsync();


            return await _context.MenusUserPrivileges.AnyAsync(x => x.ApplicationUserId == userId && x.MenuId == menuId);
        }

    }
}
