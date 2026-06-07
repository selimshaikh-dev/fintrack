using FinTrack.Application.AuthRole.Interfaces;
using FinTrack.Application.AuthRole.ViewModels;
using FinTrack.Application.Common.Models;
using FinTrack.Domain.Constants;
using FinTrack.Domain.Entities.Auth.AuthRole;
using FinTrack.Infrastructure.Data;
using FinTrack.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FinTrack.Infrastructure.Services.Jamuna
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;
        public RoleService(ApplicationDbContext context, RoleManager<ApplicationRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
        }
        public async Task<ResultModel> CreateRole(RoleVM rolevm)
        {
            ResultModel resultModel = new ResultModel();
            var isExist = await _roleManager.RoleExistsAsync(rolevm.Name);
            if (isExist)
            {
                resultModel.Succeed = false;
                resultModel.Message = $"Role {rolevm.Name} already exist.";
                return resultModel;
            }
            var role = new ApplicationRole
            {
                Name = rolevm.Name,
                ShownAs = rolevm.ShownAs,
                NormalizedName = rolevm.Name
            };
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                resultModel.Succeed = true;
                resultModel.Message = $"Role '{rolevm.Name}' is created successfully.";
                return resultModel;

            }
            else
            {
                resultModel.Succeed = false;
                resultModel.Message = $"Failed to create role '{rolevm.Name}'.";
                return resultModel;
            }
        }
        public async Task<ResultModel> UpdateRole(RoleUpdateVM rolevm)
        {
            ResultModel resultModel = new ResultModel();
            if (string.IsNullOrEmpty(rolevm.Id))
            {
                resultModel.Succeed = false;
                resultModel.Message = "Role Id is not found.";
                return resultModel;
            }

            var role = await _roleManager.FindByIdAsync(rolevm.Id);
            if (role == null)
            {
                resultModel.Succeed = false;
                resultModel.Message = "Role not found.";
                return resultModel;
            }

            role.Name = rolevm.Name;
            role.ShownAs = rolevm.ShownAs;
            role.NormalizedName = rolevm.Name;

            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                resultModel.Succeed = true;
                resultModel.Message = $"Role '{rolevm.Name}' is updated successfully.";
                return resultModel;

            }
            else
            {
                resultModel.Succeed = false;
                resultModel.Message = $"Failed to update role '{rolevm.Name}'.";
                return resultModel;
            }
        }

        public async Task<ResultModel> DeleteRole(string id)
        {
            ResultModel resultModel = new ResultModel();
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var isRoleUsed = await _context.UserRoles.AnyAsync(x => x.RoleId == id);
                if (isRoleUsed)
                {
                    resultModel.Succeed = false;
                    resultModel.Message = $"Role {role.Name} can not be delete because this role {role.Name} is already assigned to user.";
                    return resultModel;
                }
                else
                {
                    var result = await _roleManager.DeleteAsync(role);
                    if (result.Succeeded)
                    {
                        resultModel.Succeed = true;
                        resultModel.Message = $"Role '{role.Name}' is deleted successfully.";
                        return resultModel;

                    }
                    else
                    {
                        resultModel.Succeed = false;
                        resultModel.Message = $"Failed to delete role '{role.Name}'.";
                        return resultModel;
                    }
                }
            }
            else
            {
                resultModel.Succeed = false;
                resultModel.Message = $"Role '{role.Name}' is not found.";
                return resultModel;
            }
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }

        public async Task<IList<RoleVM>> GetRole()
        {
            var roles = await _roleManager.Roles.Select(x => new RoleVM
            {
                Id = x.Id,
                Name = x.Name,
                ShownAs = x.ShownAs,
            }).ToListAsync();
            return roles;
        }
        public async Task<RoleVM> GetRoleById(string id)
        {
            var data = await _roleManager.FindByIdAsync(id);
            var role = new RoleVM
            { Id = data.Id, Name = data.Name, ShownAs = data.ShownAs };
            return role;
        }
        public async Task<RoleVM> GetRoleByName(string name)
        {
            var data = await _roleManager.FindByNameAsync(name);
            var role = new RoleVM
            { Id = data.Id, Name = data.Name, ShownAs = data.ShownAs };
            return role;
        }
    }
}
