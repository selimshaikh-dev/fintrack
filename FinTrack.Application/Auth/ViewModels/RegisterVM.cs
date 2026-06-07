
using FinTrack.Application.Auth.Commands;
using FinTrack.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Auth.ViewModels
{
    public class RegisterVM : IMapFrom<RegisterCommand>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string NationalId { get; set; } = string.Empty;
        public string PassportNum { get; set; } = string.Empty;
        public bool TermsAndConditions { get; set; }    
        public DateTime DateOfBirth { get; set; }
        public string Site_Key { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
