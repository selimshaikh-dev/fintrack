using FinTrack.Application.Auth.ViewModels;
using FinTrack.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<object> Login(LoginVM lgoinVM);
        Task<ResultModel> RegisterUserAsync(RegisterVM register);
        Task<ResultModel> VerifyEmail(string userId, string email);
        Task<ResultModel> ForgetPassword(string email);
        Task<Result> ChangePassword(string oldPassword, string newPassword);
        Task<ResultModel> SetPassword(string id,string password);
        Task<bool> IsInRoleAsync(string userId, string role);
        Task<bool> AuthorizeAsync(string userId, string policyName);
        Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);
        Task<Result> DeleteUserAsync(string userId);
        Task<string> GetUserNameAsync(string userId);
    }
}
