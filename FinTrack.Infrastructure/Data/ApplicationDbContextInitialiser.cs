using FinTrack.Application.Common.Interfaces;
using FinTrack.Domain.Entities.Auth.AuthRole;
using FinTrack.Domain.Entities.Auth.AuthUser;
using FinTrack.Domain.Entities.Auth.AuthViews;
using FinTrack.Domain.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace FinTrack.Infrastructure.Data
{
    public static class InitialiserExtensions
    {
        public static async Task InitialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
            var actionDescriptor = scope.ServiceProvider.GetRequiredService<IActionDescriptorCollectionProvider>();

            await initialiser.InitialiseAsync();

            await initialiser.SeedAsync(actionDescriptor);
        }
    }

    public class ApplicationDbContextInitialiser
    {
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IServerDateTimeService _serverDateTimeService;

        public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IServerDateTimeService serverDateTimeService)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _serverDateTimeService = serverDateTimeService;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();              
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync(IActionDescriptorCollectionProvider actionDescriptor)
        {
            try
            {
                await TrySeedAsync(actionDescriptor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync(IActionDescriptorCollectionProvider actionDescriptor)
        {
            if (!_userManager.Users.Any())
            {
                DateTime currentServerDatetime =await _serverDateTimeService.GetServerDateTimeAsync();
                // Create some roles
                var roles = new List<ApplicationRole>
                {
                    new ApplicationRole {Name = UsersRole.Developer.ToString(),NormalizedName = UsersRole.Developer.ToString(),ShownAs = UsersRole.Developer.ToString()},
                    new ApplicationRole {Name = UsersRole.SuperAdmin.ToString(),NormalizedName = UsersRole.SuperAdmin.ToString(),ShownAs = UsersRole.SuperAdmin.ToString()},
                    new ApplicationRole {Name = UsersRole.Admin.ToString(),NormalizedName = UsersRole.Admin.ToString(),ShownAs = UsersRole.Admin.ToString()},
                    new ApplicationRole {Name = UsersRole.GUser.ToString(),NormalizedName = UsersRole.GUser.ToString(),ShownAs = UsersRole.GUser.ToString()},
                    new ApplicationRole {Name = UsersRole.Guest.ToString(),NormalizedName = UsersRole.Guest.ToString(),ShownAs = UsersRole.Guest.ToString()},
                    new ApplicationRole {Name = UsersRole.Member.ToString(),NormalizedName = UsersRole.Member.ToString(),ShownAs = UsersRole.Member.ToString()}
                };

                foreach (var role in roles)
                {
                    if (_roleManager.Roles.All(r => r.Name != role.Name))
                    {
                        await _roleManager.CreateAsync(role);
                    }
                }

                //Create user
                var User = new ApplicationUser
                {
                    UserName = "developer@gmail.com",
                    Email = "developer@gmail.com",
                    Full_Name ="Developer",
                    Is_Active = true,
                    EmailConfirmed = true,
                    IsConfirmed = true,
                    CreatedAt = currentServerDatetime,
                    UpdateAt =currentServerDatetime,
                    
                };
                if (_userManager.Users.All(u => u.UserName != User.UserName))
                {
                    var result = _userManager.CreateAsync(User, "Developer123!").Result;
                    if (result.Succeeded)
                    {
                        var admin = _userManager.FindByNameAsync("developer@gmail.com").Result;
                        await _userManager.AddToRolesAsync(admin, new[] { UsersRole.Developer.ToString() });
                    }
                }
            }

            List<string> actionEntities = new List<string>();
            var controllerList = actionDescriptor.ActionDescriptors.Items.Select(x => x.RouteValues["Controller"]).Distinct().ToList();

            foreach (var controller in controllerList)
            {
                var actionList = actionDescriptor.ActionDescriptors.Items.Where(c => c.RouteValues["Controller"] == controller).Select(x => x.RouteValues["Action"]).ToList();
                foreach (var action in actionList)
                {
                    string actionEntity = "/" + controller + "/" + action;
                    actionEntities.Add(actionEntity);
                }
            }
            await AddActionToView(actionEntities, _context);
        }
        private async static Task AddActionToView(List<string> entities, ApplicationDbContext context)
        {
            var urls = new List<MenusUrl>();
            var DbActionList = await context.MenusUrls.Where(x => x.Deleted == false).ToListAsync();
            if (DbActionList.Count == 0)
            {
                var url = new MenusUrl { Name = "#" };
                urls.Add(url);
            }
            var NewActionList = entities.Where(x => !DbActionList.Any(p => x.Equals(p.Name))).ToList();

            foreach (var entity in NewActionList)
            {
                var url = new MenusUrl { Name = entity };
                urls.Add(url);
            }
            if (urls.Count > 0)
            {
                await context.MenusUrls.AddRangeAsync(urls);
                await context.SaveChangesAsync();
            }
        }
    }
}
