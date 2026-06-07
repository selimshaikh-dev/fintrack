using FinTrack.Application.Common.Models;
using FinTrack.Application.Requests.Jamuna.Menus_Url.Interfaces;
using FinTrack.Application.Requests.Jamuna.Menus_Url.ViewModels;
using FinTrack.Domain.Entities.Auth.AuthViews;
using FinTrack.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FinTrack.Infrastructure.Services.Jamuna
{
    public class MenusUrlService : IMenusUrlService
    {
       private readonly ApplicationDbContext _context;

        public MenusUrlService(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(_context));
        }
        public async Task<ResultModel> CreateMenusUrl(MenusUrlVM MenusUrlvm)
        {
            ResultModel resultModel = new ResultModel();
            var isExist = await _context.MenusUrls.AnyAsync(x=>x.Name.Trim().Equals(MenusUrlvm.Name.Trim()));
            if (isExist)
            {
                resultModel.Succeed = false;
                resultModel.Message = $"MenusUrl {MenusUrlvm.Name} already exist!";
                return resultModel;
            }
            var data = new MenusUrl
            {
                Name = MenusUrlvm.Name,
                Deleted = false
            };
            await _context.MenusUrls.AddAsync(data);
            if (data.Id > 0)
            {
                resultModel.Succeed = true;
                resultModel.Message = $"MenusUrl '{MenusUrlvm.Name}' is created successfully!";
                return resultModel;

            }
            else
            {
                resultModel.Succeed = false;
                resultModel.Message = $"Failed to create MenusUrl '{MenusUrlvm.Name}'!";
                return resultModel;
            }
        }
        public async Task<ResultModel> UpdateMenusUrl(MenusUrlVM MenusUrlvm)
        {
            ResultModel resultModel = new ResultModel();

            var MenusUrl = await _context.MenusUrls.FirstOrDefaultAsync(x=>x.Id == MenusUrlvm.Id);
            if (MenusUrl == null)
            {
                resultModel.Succeed = false;
                resultModel.Message = $"Menu Url '{MenusUrlvm.Name}' is not found!";
                return resultModel;
            }

            MenusUrl.Name = MenusUrlvm.Name;
            var data = await _context.SaveChangesAsync();
            if (data > 0)
            {
                resultModel.Succeed = true;
                resultModel.Message = $"MenusUrl '{MenusUrlvm.Name}' is updated successfully.";
                return resultModel;

            }
            else
            {
                resultModel.Succeed = false;
                resultModel.Message = $"Failed to update MenusUrl '{MenusUrlvm.Name}'.";
                return resultModel;
            }
        }

        public async Task<ResultModel> DeleteMenusUrl(long id)
        {
            ResultModel resultModel = new ResultModel();
            var menusUrl = await _context.MenusUrls.FindAsync(id);
            if (menusUrl != null)
            {
                var isMenusUrlUsed = await _context.Menus.AnyAsync(x=>x.MenuUrlId == id);
                if (isMenusUrlUsed)
                {
                    resultModel.Succeed = false;
                    resultModel.Message = $"Menu Url {menusUrl.Name} can not be delete because this Menu Url {menusUrl.Name} is already assigned to Menus.";
                    return resultModel;
                }
                else
                {
                    menusUrl.Deleted = true;
                    var data = await _context.SaveChangesAsync();
                    if (data > 0)
                    {
                        resultModel.Succeed = true;
                        resultModel.Message = $"MenusUrl '{menusUrl.Name}' is deleted successfully.";
                        return resultModel;

                    }
                    else
                    {
                        resultModel.Succeed = false;
                        resultModel.Message = $"Failed to delete MenusUrl '{menusUrl.Name}'.";
                        return resultModel;
                    }
                }
            }
            else
            {
                resultModel.Succeed = false;
                resultModel.Message = $"MenusUrl '{menusUrl.Name}' is not found.";
                return resultModel;
            }
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }
        public async Task<MenusUrlVM> GetMenusUrlById(long id)
        {
            var data = await _context.MenusUrls.FirstOrDefaultAsync(x => x.Id == id);
            var menusUrl = new MenusUrlVM
            { Id = data.Id, Name = data.Name};
            return menusUrl;
        }
        public async Task<MenusUrlVM> GetMenusUrlByName(string name)
        {
            var MenusUrl = new MenusUrlVM();
            var data = await _context.MenusUrls.FirstOrDefaultAsync(x => x.Name.ToLower().Trim() == name.ToLower().Trim());
            if (data != null)
            { 
                MenusUrl.Id = data.Id;
                MenusUrl.Name = data.Name;
            }
            return MenusUrl;
        }

        public async Task<IList<MenusUrlVM>> GetMemusUrl()
        {
            var MenusUrls = await _context.MenusUrls.Where(x => x.Deleted == false).Select(x => new MenusUrlVM
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();
            return MenusUrls;
        }
    }
}
