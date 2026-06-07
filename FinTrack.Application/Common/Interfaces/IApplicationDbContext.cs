using FinTrack.Domain.Entities.Auth.AuthViews;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenusGroupPrivilege> MenusGroupPrivileges { get; set; }
        public DbSet<MenusUserPrivilege> MenusUserPrivileges { get; set; }
        public DbSet<MenusUrl> MenusUrls { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
