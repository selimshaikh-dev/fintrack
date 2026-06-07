using FinTrack.Application.AuthUser.ViewModels;
using FinTrack.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthUser.Interfaces
{
    public interface IUserService:IDisposable
    {
        Task<ResultModel> CreateUserAsync(UserVM user);
        Task<ResultModel> UpdateUserAsync(UpdateUserVM user);
        Task<ResultModel> DeleteUserAsync(string id);
        Task<ResultModel> ActiveOrDeactiveUserAsync(string id, bool isActive);
        Task<UserReturnVM> GetUserByIdAsync(string id);
        Task<UserReturnVM> GetUserByEmailAsync(string email);
        Task<IEnumerable<UserReturnVM>> GetUsersAsync(UserQueryVM searchItem);
        Task<IEnumerable<UserReturnVM>> GetMembersAsync(UserQueryVM searchItem);
    }
}
