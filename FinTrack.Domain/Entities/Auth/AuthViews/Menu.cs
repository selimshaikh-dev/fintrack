using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinTrack.Domain.Enums;
using FinTrack.Domain.Common;

namespace FinTrack.Domain.Entities.Auth.AuthViews
{
    public class Menu : BaseAuditableEntity
    {
        public Menu()
        {
            MenusMulti = new HashSet<Menu>();
            MenusUserPrivileges = new HashSet<MenusUserPrivilege>();
            MenusGroupPrivileges = new HashSet<MenusGroupPrivilege>();
        }

        public long Id { get; set; }
        public long? ParentId { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public long MenuUrlId { get; set; }
        public ViewTypeEnum Type { get; set; }
        public bool IsMenuItem { get; set; }
        public bool Active { get; set; }
        public int DisplayOrder { get; set; }
        public string IconClass { get; set; }
        public bool Deleted { get; set; }
        public bool IsNewlyFeatured { get; set; }
        public DateTime? NewFeatureEndDate { get; set; }
        public ICollection<MenusUserPrivilege> MenusUserPrivileges { get; set; }
        public ICollection<MenusGroupPrivilege> MenusGroupPrivileges { get; set; }
        public ICollection<Menu> MenusMulti { get; set; }
        public virtual MenusUrl MenusUrls { get; set; }
        public Menu MenuSingle { get; set; }
        public class MenuMap : IEntityTypeConfiguration<Menu>
        {
            public void Configure(EntityTypeBuilder<Menu> builder)
            {
                builder.HasOne(x => x.MenusUrls).WithMany(x => x.Menus).HasForeignKey(x => x.MenuUrlId);
            }
        }
    }

    public class AuthViewMap : IEntityTypeConfiguration<Menu>
    {
        public void Configure(EntityTypeBuilder<Menu> builder)
        {

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(128);
            builder.Property(x => x.URL)
                .IsRequired()
                .HasMaxLength(150);
            builder.Property(x => x.URL)
                .HasMaxLength(250);
        }
    }
}
