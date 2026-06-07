using Dapper;
using FinTrack.Application.AuthUser.Interfaces;
using FinTrack.Application.AuthUser.ViewModels;
using FinTrack.Application.Common.Constants;
using FinTrack.Application.Common.Helpers;
using FinTrack.Application.Common.Interfaces;
using FinTrack.Application.Common.Models;
using FinTrack.Application.Requests.Jamuna.Client_Jamuna.Interfaces;
using FinTrack.Domain.Entities.Auth.AuthUser;
using FinTrack.Domain.Enums;
using FinTrack.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Infrastructure.Services.Jamuna
{
    public class UserService : SqlDbContext<UserReturnVM>, IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;
        private readonly IServerDateTimeService _serverDateTimeService;
        private readonly IViewRenderService _viewRenderService;
        private readonly IEmailService _emailService;
        private readonly IUser _user;
        private readonly IClientServiceJamuna _clientService;
        private readonly IIdentityService _identityService;
        public UserService(IConfiguration configuration, UserManager<ApplicationUser> userManager, ApplicationDbContext context,
            IConfiguration config, IServerDateTimeService serverDateTimeService, IViewRenderService viewRenderService, IEmailService emailService, IUser user, IClientServiceJamuna clientService, IIdentityService identityService) : base(configuration)
        {
            _userManager = userManager;
            _context = context;
            _config = config;
            _serverDateTimeService = serverDateTimeService;
            _viewRenderService = viewRenderService;
            _emailService = emailService;
            _user = user;
            _clientService = clientService;
            _identityService = identityService;
        }

        [Obsolete]
        public async Task<ResultModel> CreateUserAsync(UserVM user)
        {
            try
            {
                ResultModel resultModel = new ResultModel();
                var clientInfos = await _clientService.GetClientInfoInPlutoByEmail(user.Email);
                var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == user.RoleId);
                if (clientInfos == null)
                {
                    if (role.Name != UsersRole.Guest.ToString())
                    {
                        resultModel.Succeed = false;
                        resultModel.Message = "Please register yourself as an employee of Jamuna using Pluto to be an user of this system.";
                        return resultModel;
                    }
                }
                else
                {
                    if (role.Name == UsersRole.Member.ToString())
                    {
                        if (string.IsNullOrEmpty(clientInfos.JamunaMemberCode))
                        {
                            resultModel.Succeed = false;
                            resultModel.Message = "Please register yourself as a member of Jamuna using Pluto to be an user of this system.";
                            return resultModel;
                        }
                    }
                }

                var userDetails = await _userManager.FindByEmailAsync(user.Email);
                if (userDetails != null)
                {
                    resultModel.Succeed = false;
                    resultModel.Message = $"User {user.Email} already exist!";
                    return resultModel;
                }

                var currentdate = await _serverDateTimeService.GetServerDateTimeAsync();
                var applicationUser = new ApplicationUser
                {
                    UserName = user.Email.Trim(' '),
                    Email = user.Email.Trim(' '),
                    Is_Migrated = true,
                    Is_Active = false,
                    Full_Name = user.Name,
                    PhoneNumber = user.ContactNumber,
                    National_Id_No = user.NationalId,
                    Passport_No = user.PassportNumber,
                    Date_Of_Birth = null,
                    CreatedAt = currentdate,
                    UpdateAt = currentdate,
                    LockoutEnabled = false,
                    NormalizedEmail = user.Email.Trim(' '),
                    NormalizedUserName = user.Email.Trim(' '),
                    CompanyId = 180,
                    IsConfirmed = false,
                    IsBlock = false,
                    EmailConfirmed = false,
                    BP_ID = clientInfos != null ? clientInfos.BP_ID_Jamuna : null
                };
                var password = PasswordHelper.Create(10);
                var createAcc = await _userManager.CreateAsync(applicationUser, password);
                if (createAcc.Succeeded)
                {
                    var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
                    var userForRole = await _userManager.FindByNameAsync(applicationUser.UserName);
                    await _userManager.AddToRoleAsync(userForRole, role.Name);

                    /////////////////////////Bellow part is used for Email Send Purpose////////////////////////////////////////
                    string encryptedUserEmail = System.Web.HttpUtility.UrlEncode(CryptoHelpers.EncryptStringAES(user.Email, AppConstant.CryptoSecret));
                    string encryptedUserId = System.Web.HttpUtility.UrlEncode(CryptoHelpers.EncryptStringAES(userForRole.Id, AppConstant.CryptoSecret));
                    string encryptedOldPassword = System.Web.HttpUtility.UrlEncode(CryptoHelpers.EncryptStringAES(password, AppConstant.CryptoSecret));

                    var callBackUrl = $"{AppConstant.BaseUrl}/Auth/VarifyEmail/{encryptedUserId}/{encryptedUserEmail}";
                    var callBackUrlForChangedPassword = $"{AppConstant.BaseUrl}/Auth/ChangedPassword/{encryptedUserEmail}";
                    var Subject = AppConstant.AppName + " - Account verification and Set Password.";
                    var SubjectLineForTemplate = "Account verification and Set Password";

                    var emailBodyVM = new EmailBodyVM
                    {
                        CallBackUrl = callBackUrl,
                        CallBackUrlForChangedPassword = callBackUrlForChangedPassword,
                        EmailConfirmationToken = confirmationToken,
                        OldPassword = password
                    };
                    var emailBody = await _viewRenderService.RenderToStringAsync("Auth/VerifyEmailPassword", emailBodyVM);

                    var emailTemplateVM = new EmailTemplateVM
                    {
                        SubjectLineForTemplate = SubjectLineForTemplate,
                        Body = emailBody
                    };
                    var emailTemplate = await _viewRenderService.RenderToStringAsync("Auth/EmailTemplate", emailTemplateVM);

                    Thread mailThread = new Thread(() =>
                    {
                        Task.Run(
                            async () => await _emailService.SendWithAwsAsync(AppConstant.AppName, user.Email.Trim(), Subject, emailTemplate, SubjectLineForTemplate, ""));
                    });
                    mailThread.Start();

                    resultModel.Succeed = true;
                    resultModel.Message = "User Created Successfully.";

                    return resultModel;
                }
                else
                {
                    resultModel.Succeed = false;
                    resultModel.Message = "Failed to Create User!";
                    return resultModel;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<ResultModel> UpdateUserAsync(UpdateUserVM user)
        {
            try
            {
                ResultModel resultModel = new ResultModel();
                var currentdate = await _serverDateTimeService.GetServerDateTimeAsync();
                var userDetails = await _userManager.FindByIdAsync(user.Id);
                if (userDetails == null)
                {
                    resultModel.Succeed = false;
                    resultModel.Message = "User not found!";
                    return resultModel;
                }
                var role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == user.RoleId);
                if (role == null)
                {
                    resultModel.Succeed = false;
                    resultModel.Message = "Role can not be updated successfully. Role does not exist!";
                    return resultModel;
                }
                if (role.Name == UsersRole.Member.ToString())
                {
                    var clientInfos = await _clientService.GetClientInfoInPlutoByEmail(user.Email);
                    if (string.IsNullOrEmpty(clientInfos.JamunaMemberCode))
                    {
                        resultModel.Succeed = false;
                        resultModel.Message = "Please register yourself as a member of Jamuna using Pluto to be an user of this system.";
                        return resultModel;
                    }
                }

                userDetails.Email = user.Email;
                userDetails.Full_Name = user.Name;
                userDetails.PhoneNumber = user.ContactNumber;
                userDetails.National_Id_No = user.NationalId;
                userDetails.Passport_No = user.PassportNumber;
                userDetails.UpdateAt = currentdate;
                userDetails.UpdateBy = _user.Id;

                var updateResult = await _userManager.UpdateAsync(userDetails);
                if (updateResult.Succeeded)
                {
                    var userRoles = await _userManager.GetRolesAsync(userDetails);
                    if (userRoles.Count > 0)
                    {
                        await _userManager.RemoveFromRolesAsync(userDetails, userRoles);
                    }
                    var roleUpdateResult = await _userManager.AddToRoleAsync(userDetails, role.Name);
                    if (roleUpdateResult.Succeeded)
                    {
                        resultModel.Succeed = true;
                        resultModel.Message = "User information updated successfully!";
                        return resultModel;
                    }
                    else
                    {
                        resultModel.Succeed = false;
                        resultModel.Message = "User role could not be updated!";
                        return resultModel;
                    }
                }
                else
                {
                    resultModel.Succeed = false;
                    resultModel.Message = "User role could not be updated!";
                    return resultModel;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<ResultModel> DeleteUserAsync(string id)
        {
            ResultModel resultModel = new ResultModel();
            var userDetails = await _userManager.FindByIdAsync(id);
            if (userDetails == null)
            {
                resultModel.Succeed = false;
                resultModel.Message = "User not found!";
                return resultModel;
            }

            var currentdate = await _serverDateTimeService.GetServerDateTimeAsync();
            userDetails.IsBlock = true;
            userDetails.Is_Active = false;
            userDetails.UpdateBy = _user.Id;
            userDetails.UpdateAt = currentdate;

            var updateResult = await _userManager.UpdateAsync(userDetails);
            if (updateResult.Succeeded)
            {
                resultModel.Succeed = true;
                resultModel.Message = "User blocked successfully!";
                return resultModel;
            }
            else
            {
                resultModel.Succeed = false;
                resultModel.Message = "User could not be deleted!";
                return resultModel;
            }
        }
        public async Task<ResultModel> ActiveOrDeactiveUserAsync(string id, bool isActive)
        {
            ResultModel resultModel = new ResultModel();
            var userDetails = await _userManager.FindByIdAsync(id);
            if (userDetails == null)
            {
                resultModel.Succeed = false;
                resultModel.Message = "User not found!";
                return resultModel;
            }

            var currentdate = await _serverDateTimeService.GetServerDateTimeAsync();
            if (isActive)
            {
                userDetails.Is_Active = true;
            }
            else
            {
                userDetails.Is_Active = false;
            }
            userDetails.UpdateBy = _user.Id;
            userDetails.UpdateAt = currentdate;

            var updateResult = await _userManager.UpdateAsync(userDetails);
            if (updateResult.Succeeded)
            {
                resultModel.Succeed = true;
                resultModel.Message = $"User {(isActive == true ? "Activate" : "Deactivate")} successfully.";
                return resultModel;
            }
            else
            {
                resultModel.Succeed = false;
                resultModel.Message = $"User could not be {(isActive == true ? "Activate" : "Deactivate")}.";
                return resultModel;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<UserReturnVM> GetUserByIdAsync(string id)
        {
            UserReturnVM objuser = new UserReturnVM();
            var userDetails = await _userManager.FindByIdAsync(id);
            if (userDetails == null)
                return objuser;

            var userRole = await _context.UserRoles.Include(x => x.Role).FirstOrDefaultAsync(x => x.UserId == userDetails.Id);

            objuser.Id = userDetails.Id;
            objuser.Email = userDetails.Email ?? "";
            objuser.Name = userDetails.Full_Name ?? "";
            objuser.ContactNumber = userDetails.PhoneNumber ?? "";
            objuser.UserGroup = userRole.Role == null ? "" : userRole.Role.Name;
            objuser.RoleId = userRole.RoleId;
            objuser.IsActive = userDetails.Is_Active;
            objuser.NationalId = userDetails.National_Id_No ?? "";
            objuser.PassportNumber = userDetails.Passport_No ?? "";

            return objuser;
        }
        public async Task<UserReturnVM> GetUserByEmailAsync(string email)
        {
            UserReturnVM objuser = new UserReturnVM();
            var userDetails = await _userManager.FindByEmailAsync(email);
            if (userDetails == null)
                return objuser;

            var userRole = await _context.UserRoles.Include(x => x.Role).FirstOrDefaultAsync(x => x.UserId == userDetails.Id);

            objuser.Id = userDetails.Id;
            objuser.Email = userDetails.Email ?? "";
            objuser.Name = userDetails.Full_Name ?? "";
            objuser.ContactNumber = userDetails.PhoneNumber ?? "";
            objuser.UserGroup = userRole.Role == null ? "" : userRole.Role.Name;
            objuser.RoleId = userRole.RoleId;
            objuser.IsActive = userDetails.Is_Active;
            objuser.NationalId = userDetails.National_Id_No ?? "";
            objuser.PassportNumber = userDetails.Passport_No ?? "";

            return objuser;
        }
        public async Task<IEnumerable<UserReturnVM>> GetUsersAsync(UserQueryVM searchItem)
        {
            string query = "Get_Users";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@PageIndex", searchItem.PageNumber, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@PageSize", searchItem.PageSize, DbType.Int32, ParameterDirection.Input);
            var users = await GetListBySPAsync(query, parameter);
            return users;
        }
        public async Task<IEnumerable<UserReturnVM>> GetMembersAsync(UserQueryVM searchItem)
        {
            string query = "Get_Members";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@PageIndex", searchItem.PageNumber, DbType.Int32, ParameterDirection.Input);
            parameter.Add("@PageSize", searchItem.PageSize, DbType.Int32, ParameterDirection.Input);
            var users = await GetListBySPAsync(query, parameter);
            return users;
        }

    }
}
