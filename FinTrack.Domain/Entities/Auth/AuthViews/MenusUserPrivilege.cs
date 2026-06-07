using FinTrack.Domain.Common;
using FinTrack.Domain.Entities.Auth.AuthUser;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Domain.Entities.Auth.AuthViews
{
    public class MenusUserPrivilege : BaseAuditableEntity
    {
        public long Id { get; set; }
        public long MenuId { get; set; }
        public string ApplicationUserId { get; set; }
        public bool CanCreate { get; set; }
        public bool CanRead { get; set; }
        public bool CanUpdate { get; set; }
        public bool CanDelete { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Menu Menus { get; set; }

    }
    public class MenusUserPrivilegeMap : IEntityTypeConfiguration<MenusUserPrivilege>
    {
        public void Configure(EntityTypeBuilder<MenusUserPrivilege> builder)
        {
            builder.HasOne(x => x.ApplicationUser).WithMany(x => x.MenusUserPrivileges).HasForeignKey(x => x.ApplicationUserId);
            builder.HasOne(x => x.Menus).WithMany(x => x.MenusUserPrivileges).HasForeignKey(x => x.MenuId);
        }
    }
}
