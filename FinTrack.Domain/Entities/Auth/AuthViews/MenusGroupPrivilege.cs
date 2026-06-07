using FinTrack.Domain.Common;
using FinTrack.Domain.Entities.Auth.AuthRole;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Domain.Entities.Auth.AuthViews
{
    public class MenusGroupPrivilege : BaseAuditableEntity
    {
        public long Id { get; set; }

        public long MenuId { get; set; }

        public string AuthRoleId { get; set; }
        public virtual ApplicationRole AuthRole { get; set; }
        public virtual Menu Menus { get; set; }

        public class AuthViewGroupPrivilegeMap : IEntityTypeConfiguration<MenusGroupPrivilege>
        {
            public void Configure(EntityTypeBuilder<MenusGroupPrivilege> builder)
            {
                builder.HasOne(x => x.AuthRole).WithMany(x => x.MenusGroupPrivileges).HasForeignKey(x => x.AuthRoleId);
                builder.HasOne(x => x.Menus).WithMany(x => x.MenusGroupPrivileges).HasForeignKey(x => x.MenuId);
            }
        }
    }
}
