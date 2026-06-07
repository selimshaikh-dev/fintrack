using FinTrack.Domain.Entities.Auth.AuthUser;
using FinTrack.Domain.Entities.Auth.AuthViews;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Domain.Entities.Auth.AuthRole
{
    public class ApplicationRole : IdentityRole<string>
    {
        public ApplicationRole()
        {
            UserRoles = new HashSet<ApplicationUserRole>();
            MenusGroupPrivileges = new HashSet<MenusGroupPrivilege>();
        }
        public string ShownAs { get; set; } = string.Empty;
        public string Discriminator { get; set; } = string.Empty;
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public virtual ICollection<MenusGroupPrivilege> MenusGroupPrivileges { get; set; }
    }
}
