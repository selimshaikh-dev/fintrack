using FinTrack.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthUser.ViewModels
{
    public class UserReturnVM 
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string UserGroup { get; set; }
        public string RoleId { get; set; }
        public bool IsActive { get; set; }
        public string NationalId { get; set; }
        public bool IsBlock { get; set; }
        public string PassportNumber { get; set; }
        public string SrcSet { get; set; } = "https://jamunaapi.globedse.com/Images/wanna1.png";
        public string SrcEmail { get; set; } = "https://jamunaapi.globedse.com/wanna1.webp";
    }
}
