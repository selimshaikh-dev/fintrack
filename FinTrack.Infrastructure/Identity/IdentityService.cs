using Ardalis.GuardClauses;
using Azure.Core;
using FinTrack.Application.Auth.ViewModels;
using FinTrack.Application.Common.Constants;
using FinTrack.Application.Common.Helpers;
using FinTrack.Application.Common.Interfaces;
using FinTrack.Application.Common.Models;
using FinTrack.Application.Requests.Jamuna.Client_Jamuna.Interfaces;
using FinTrack.Application.Requests.Jamuna.Employee.Interfaces;
using FinTrack.Domain.Entities.Auth.AuthRole;
using FinTrack.Domain.Entities.Auth.AuthUser;
using FinTrack.Domain.Enums;
using FinTrack.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FinTrack.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
        private readonly IAuthorizationService _authorizationService;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly IServerDateTimeService _serverDateTimeService;
        private readonly IViewRenderService _viewRenderService;
        private readonly IEmailService _emailService;
        private readonly IUser _user;
        private readonly IClientServiceJamuna _clientService;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IEmployeeService _employeeService;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
            IAuthorizationService authorizationService, ApplicationDbContext context, 
            SignInManager<ApplicationUser> signInManager,IConfiguration config,
            IServerDateTimeService serverDateTimeService, IViewRenderService viewRenderService,
            IEmailService emailService, IUser user, IClientServiceJamuna clientService,
            IEmployeeService employeeService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _authorizationService = authorizationService;
            _config = config;
            _serverDateTimeService = serverDateTimeService;
            _viewRenderService = viewRenderService;
            _emailService = emailService;
            _user = user;
            _clientService = clientService;
            _roleManager = roleManager;
            _employeeService = employeeService;
        }

        public async Task<object> Login(LoginVM loginVM)
        {
            if (!string.IsNullOrWhiteSpace(loginVM.Email) && !string.IsNullOrWhiteSpace(loginVM.Password))
            {
                var userProfile = await _userManager.FindByEmailAsync(loginVM.Email) ?? await _userManager.FindByNameAsync(loginVM.Email);
                if (userProfile == null)
                {
                    return (Result.Failure(new List<string> { "The username or password provided is incorrect!" }));
                }
                if (userProfile.IsBlock)
                {
                    return (Result.Failure(new List<string> { "User is deleted!" }));
                }
                if (!userProfile.Is_Active)
                {
                    return (Result.Failure(new List<string> { "User not active yet!" }));
                }
                if (!userProfile.EmailConfirmed)
                {
                    return (Result.Failure(new List<string> { "Your account is not confirmed yet. Please check your email and follow the instructions to activate your account!" }));
                }
                var userRoleName =  await (from a in _context.UserRoles
                                    join b in _context.Roles on a.RoleId equals b.Id
                                    where a.UserId == userProfile.Id
                                    select b.Name).FirstOrDefaultAsync();
                if (string.IsNullOrEmpty(userRoleName) )
                {
                    return (Result.Failure(new List<string> { "Your account is not found in any group yet!" }));
                }
                var result = await _signInManager.CheckPasswordSignInAsync(userProfile, loginVM.Password, false);
                if (result.Succeeded)
                {
                    var isEmailExistsAsClient = await _clientService.GetClientInfoInPlutoByEmail(loginVM.Email.Trim(' '));
                    if (userRoleName == UsersRole.Guest.ToString() || userRoleName == UsersRole.Member.ToString())
                    {
                        if (isEmailExistsAsClient == null)
                        {
                            if (userRoleName == UsersRole.Member.ToString())
                            {
                                var roles = await _userManager.GetRolesAsync(userProfile);
                                await _userManager.RemoveFromRolesAsync(userProfile, roles);
                                await _userManager.AddToRoleAsync(userProfile, UsersRole.Guest.ToString());
                                userRoleName = UsersRole.Guest.ToString();

                            }
                        }
                        else
                        {
                            if (userRoleName == UsersRole.Guest.ToString())
                            {
                                var roles = await _userManager.GetRolesAsync(userProfile);
                                await _userManager.RemoveFromRolesAsync(userProfile, roles);
                                await _userManager.AddToRoleAsync(userProfile, UsersRole.Member.ToString());
                                userRoleName= UsersRole.Member.ToString();
                            }
                        }
                    }
                    else
                    {
                        if (userRoleName != UsersRole.Developer.ToString())
                        {
                            if (isEmailExistsAsClient != null)
                            {
                                var employeeInfos = await _employeeService.GetEmployeeByBpIdAsync(isEmailExistsAsClient.BP_ID_Jamuna);
                                if (employeeInfos == null)
                                {
                                    return (Result.Failure(new List<string> { "Employee's information is not found!" }));
                                }

                            }
                            else
                            {
                                return (Result.Failure(new List<string> { "Email is not exist!" }));
                            }
                        }
                    }
                    LginUserVM appUser = new LginUserVM
                    {
                        Id = userProfile.Id ?? "",
                        UserName = userProfile.UserName ?? "",
                        Email = userProfile.Email ??"",
                        Name = userProfile.Full_Name ?? "",
                        UserGroup = userRoleName ?? ""
                    };

                    return new
                    {
                        token = GenerateJwtToken(userProfile).Result,
                        user = appUser,
                        Succeed = true
                    };
                }
                return (Result.Failure(new List<string> { "Invalid Email or Password!" }));
            }
            return (Result.Failure(new List<string> { "Email or Password can not be empty!" }));
        }

        [Obsolete]
        public async Task<ResultModel> RegisterUserAsync(RegisterVM register)
        {
            ResultModel objResult = new ResultModel();
            try
            {
                var checkUser = await _context.Users.FirstOrDefaultAsync(c => c.Email == register.Email.Trim(' '));
                if (checkUser != null)
                {
                    if (checkUser.Is_Active == false)
                    {
                        objResult.Succeed = false;
                        objResult.Message = "Your account already exists but not active. Please contact with Jamuna Sanchoy and Rindan Samobai Samiti Ltd. to activate your account!";
                        return objResult;
                        
                    }
                    else if (checkUser.EmailConfirmed == false)
                    {
                        objResult.Succeed = false;
                        objResult.Message = "Your account already exists but email not confirm. Please contact with Jamuna Sanchoy and Rindan Samobai Samiti Ltd. to confirm your email!";
                        return objResult;
                    }
                    else if (checkUser.IsBlock)
                    {
                        objResult.Succeed = false;
                        objResult.Message = "User is blocked!";
                        return objResult;
                    }
                    else
                    {
                        objResult.Succeed = false;
                        objResult.Message = "This mail already exist!";
                        return objResult;
                    }
                }

                var isEmailExistsAsClient = await _clientService.GetClientInfoInPlutoByEmail(register.Email.Trim(' '));
                register.Email = register.Email.Trim(' ');

                DateTime createdDate = await _serverDateTimeService.GetServerDateTimeAsync();

                var user = new ApplicationUser()
                {
                    UserName = register.Email.Trim(),
                    Email = register.Email,
                    Is_Migrated = true,
                    Is_Active = false,
                    Full_Name = register.FullName,
                    PhoneNumber = register.PhoneNumber,
                    National_Id_No = register.NationalId,
                    Passport_No = register.PassportNum,
                    Date_Of_Birth = register.DateOfBirth,
                    CreatedAt = createdDate,
                    UpdateAt = createdDate,
                    LockoutEnabled = false,
                    NormalizedEmail = register.Email,
                    NormalizedUserName = register.Email.Trim(),
                    CompanyId = 180,
                    IsConfirmed = false,
                    IsBlock = false,
                    EmailConfirmed = false,
                    BP_ID = isEmailExistsAsClient != null ? isEmailExistsAsClient.BP_ID_Jamuna : null 
                };

                var createAcc = await _userManager.CreateAsync(user, register.Password);
                if (createAcc.Succeeded)
                {
                    var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var userForRole = await _userManager.FindByNameAsync(user.UserName);
                    await AssignRoleToUser(userForRole, user.BP_ID);

                    /////////////////////////Bellow part is used for Email Send Purpose////////////////////////////////////////
                    string encryptedUserEmail = System.Web.HttpUtility.UrlEncode(CryptoHelpers.EncryptStringAES(register.Email, AppConstant.CryptoSecret));
                    string encryptedUserId = System.Web.HttpUtility.UrlEncode(CryptoHelpers.EncryptStringAES(user.Id, AppConstant.CryptoSecret));

                    var callBackUrl = $"{AppConstant.BaseUrl}/verify-email/{encryptedUserId}/{encryptedUserEmail}";
                    var Subject = AppConstant.AppName + " - Account Confirmation";
                    var SubjectLineForTemplate = "Confirm your account";
                    
                    var emailBodyVM = new EmailBodyVM
                    {
                        CallBackUrl = callBackUrl,
                        EmailConfirmationToken = confirmationToken
                    };
                    var emailBody = await _viewRenderService.RenderToStringAsync("Auth/VerifyEmail", emailBodyVM);

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

                    objResult.Succeed = true;
                    objResult.Id = user.Id;
                    objResult.Message = "Account created successfully. Please check your email to gain access into your account. Email verification link sent to your inbox will be expired within 7 days, so please complete the process within this time..";
                    return objResult;
                }
                else
                {
                    objResult.Succeed = false;
                    objResult.Message = "User not created. Something went worng!";
                    return objResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        [Obsolete]
        public async Task<ResultModel> VerifyEmail(string id, string email)
        {
            ResultModel objResult = new ResultModel();
            string userid = CryptoHelpers.DecryptStringAES(id.Contains('%') ? HttpUtility.UrlDecode(id) : id, AppConstant.CryptoSecret);
            string useremail = CryptoHelpers.DecryptStringAES(email.Contains('%') ? HttpUtility.UrlDecode(email) : email, AppConstant.CryptoSecret);

            try
            {
                var user = await _context.Users.Where(x => x.Id == userid).FirstOrDefaultAsync();
                if (user == null)
                {
                    objResult.Succeed = false;
                    objResult.Message = "User not found!";
                    return objResult;
                }
                else
                {
                    if (user.Email.Trim().ToLower() != useremail.Trim().ToLower())
                    {
                        objResult.Succeed = false;
                        objResult.Message = "Email not found!";
                        return objResult;
                    }
                    if (user.EmailConfirmed)
                    {
                        objResult.Succeed = false;
                        objResult.Message = "Email already varifyed!";
                        return objResult;  
                    }
                    user.Is_Active = true;
                    user.IsConfirmed = true;
                    user.EmailConfirmed = true;
                    await _context.SaveChangesAsync(CancellationToken.None);
                    objResult.Succeed = true;
                    objResult.Message = "Email Successfully Varified.";
                    return objResult;
                }

            }
            catch (Exception ex)
            {
                objResult.Succeed = true;
                objResult.Message = ex.Message;
                return objResult;
            }
        }

        [Obsolete]
        public async Task<ResultModel> ForgetPassword(string email)
        {
            ResultModel objResult = new ResultModel();
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                objResult.Succeed = false;
                objResult.Message = "User not found!";
                return objResult;
            }
            string encryptedUserId = System.Web.HttpUtility.UrlEncode(CryptoHelpers.EncryptStringAES(user.Id, AppConstant.CryptoSecret));
            string encryptedUserEmail = System.Web.HttpUtility.UrlEncode(CryptoHelpers.EncryptStringAES(user.Email, AppConstant.CryptoSecret));

            var callBackUrl = $"{AppConstant.BaseUrl}/reset-password/{encryptedUserId}";
            var Subject = AppConstant.AppName + " - Reset your Password.";
            var SubjectLineForTemplate = "Reset your Password";

            var emailBodyVM = new EmailBodyVM
            {
                CallBackUrl = callBackUrl
            };
            var emailBody = await _viewRenderService.RenderToStringAsync("Auth/ForgetPassword", emailBodyVM);

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

            objResult.Succeed = true;
            objResult.Message = "Please check your email to gain access into your account. Password reset link sent to your inbox will be expired within 7 days, so please complete the process within this time..";
            return objResult;
        }
        public async Task<Result> ChangePassword(string oldPassword, string newPassword)
        {
            var currentUserId = _user.Id;
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == currentUserId && !x.IsBlock);
            if (!user.Is_Active )
            {
                throw new UnauthorizedAccessException("User not active yet!!!");
            }
            if (user == null)
                return (Result.Failure(new List<string> { "User Not Found!!" }));
            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

            return Result.Success("Successfully Changed..");
        }

        [Obsolete]
        public async Task<ResultModel> SetPassword(string id, string password)
        {
            ResultModel objResult = new ResultModel();
            try
            {
                string userid = CryptoHelpers.DecryptStringAES(id.Contains('%') ? HttpUtility.UrlDecode(id) : id, AppConstant.CryptoSecret);
                var user = await _userManager.FindByIdAsync(userid);
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                if (user == null)
                {
                    objResult.Succeed = false;
                    objResult.Message = "User not found!";
                    return objResult;
                }
                var result = await _userManager.ResetPasswordAsync(user, token, password);
                if (result.Succeeded)
                {
                    objResult.Succeed = true;
                    objResult.Message = "Password Changed Successfully.";
                    return objResult;
                }
                else
                {
                    objResult.Succeed = true;
                    objResult.Message = "Failed to Set New password!";
                    return objResult;
                }
            }
            catch (Exception e)
            {
                objResult.Succeed = false;
                objResult.Message = "Something went worng! " + e.Message;
                return objResult;
            }
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
                new Claim(ClaimTypes.GivenName, user.Full_Name ?? ""),
                new Claim(ClaimTypes.Email, user.Email ??""),
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes("ecawiasqrpqrgyhwnolrudpbsrwaynbqdayndnmcehjnwqyouikpodzaqxivwkconwqbhrmxfgccbxbyljguwlxhdlcvxlutbnwjlgpfhjgqbegtbxbvwnacyqnltrby"));
            var signIn = new SigningCredentials
                         (key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: "http://localhost:5000;https://localhost:5001;",
                audience: "http://localhost:5000;https://localhost:5001;",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signIn);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        public async Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = userName,
            };

            var result = await _userManager.CreateAsync(user, password);

            return (result.ToApplicationResult(), user.Id);
        }

        public async Task<bool> IsInRoleAsync(string userId, string role)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null && await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> AuthorizeAsync(string userId, string policyName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return false;
            }

            var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

            var result = await _authorizationService.AuthorizeAsync(principal, policyName);

            return result.Succeeded;
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            return user != null ? await DeleteUserAsync(user) : Result.Success();
        }

        public async Task<Result> DeleteUserAsync(ApplicationUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }
        private async Task AssignRoleToUser(ApplicationUser applicationUser , int? bpid)
        {
            if (bpid == null || string.IsNullOrEmpty(bpid.ToString()))
            {
                await _userManager.AddToRoleAsync(applicationUser, UsersRole.Guest.ToString());
            }
            else
            {
                await _userManager.AddToRoleAsync(applicationUser, UsersRole.Member.ToString());
            }
        }

    }
}
