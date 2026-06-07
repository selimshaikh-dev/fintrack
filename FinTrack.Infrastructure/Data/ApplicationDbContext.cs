using FinTrack.Application.Common.Interfaces;
using FinTrack.Domain.Entities.Auth.AuthRole;
using FinTrack.Domain.Entities.Auth.AuthUser;
using FinTrack.Domain.Entities.Auth.AuthViews;
using FinTrack.Domain.Entities.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>,
        ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenusGroupPrivilege> MenusGroupPrivileges { get; set; }
        public DbSet<MenusUserPrivilege> MenusUserPrivileges { get; set; }
        public DbSet<MenusUrl> MenusUrls { get; set; }
        public DbSet<UserToMemberMapping> UserToMemberMappings { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            builder.Entity<ApplicationRole>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });
            builder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
            builder.Entity<Menu>(menu =>
            {
                menu.HasOne(ur => ur.MenuSingle)
                    .WithMany(r => r.MenusMulti)
                    .HasForeignKey(ur => ur.ParentId)
                    .IsRequired(false);
            });
        }
    }
}
