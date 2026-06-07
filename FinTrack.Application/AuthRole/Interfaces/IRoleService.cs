using FinTrack.Application.AuthRole.ViewModels;
using FinTrack.Application.Common.Models;
using FinTrack.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthRole.Interfaces
{
    public interface IRoleService : IDisposable
    {
        public Task<ResultModel> CreateRole(RoleVM rolevm);
        public Task<ResultModel> UpdateRole(RoleUpdateVM rolevm);
        public Task<ResultModel> DeleteRole(string id);
        public Task<IList<RoleVM>> GetRole();
        public Task<RoleVM> GetRoleById(string id);
        public Task<RoleVM> GetRoleByName(string name);
    }
}
