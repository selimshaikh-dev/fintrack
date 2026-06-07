using FinTrack.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Auth.Commands
{
    public class RegisterCommand : IRequest<ResultModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public string PassportNum { get; set; }
        public bool TermsAndConditions { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Site_Key { get; set; }
        public string Token { get; set; }

        public RegisterCommand(string email, string password, string fullName, string phoneNumber, string nationalId, string passportNum, bool termsAndConditions, DateTime dateOfBirth, string site_Key, string token)
        {
            Email = email;
            Password = password;
            FullName = fullName;
            PhoneNumber = phoneNumber;
            NationalId = nationalId;
            PassportNum = passportNum;
            TermsAndConditions = termsAndConditions;
            DateOfBirth = dateOfBirth;
            Site_Key = site_Key;
            Token = token;             
        }
    }
}
