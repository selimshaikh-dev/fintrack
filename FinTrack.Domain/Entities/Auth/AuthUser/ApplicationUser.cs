using FinTrack.Domain.Entities.Auth.AuthRole;
using FinTrack.Domain.Entities.Auth.AuthViews;
using FinTrack.Domain.Entities.Mapping;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Domain.Entities.Auth.AuthUser
{
    public class ApplicationUser : IdentityUser<string>
    {
        public ApplicationUser()
        {
            UserRoles = new HashSet<ApplicationUserRole>();
            MenusUserPrivileges = new HashSet<MenusUserPrivilege>();           
        }
        public string Full_Name { get; set; } = string.Empty;
        public string National_Id_No { get; set; } = string.Empty;
        public string Passport_No { get; set; } = string.Empty;
        public Nullable<DateTime> Date_Of_Birth { get; set; }
        public Nullable<int> BP_ID { get; set; }
        public bool Is_Migrated { get; set; }
        public bool Is_Active { get; set; }
        public string ProfilePictureName { get; set; } = string.Empty;
        public string ConfirmationToken { get; set; } = string.Empty;
        public DateTime LastPasswordFailureDate { get; set; }
        public bool PasswordFailuresSinceLastSuccess { get; set; }
        public DateTime PasswordChangedDate { get; set; }
        public string PasswordSalt { get; set; } = string.Empty;
        public string PasswordVerificationToken { get; set; } = string.Empty;
        public DateTime PasswordVerificationTokenExpirationDate { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime LockoutEndDateUtc { get; set; }
        public int CompanyId { get; set; }
        public bool IsBlock { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<DateTime> UpdateAt { get; set; }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public virtual ICollection<MenusUserPrivilege> MenusUserPrivileges { get; set; }
    }
}
