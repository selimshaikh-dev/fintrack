using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Common.Helpers
{
    public class ValidationCheckerHelper
    {
        public static bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public static bool IsValidName(string name)
        {
            return name.All(Char.IsLetter);
        }
        public static bool IsValidNumber(string number)
        {
            return number.All(Char.IsNumber);
        }
    }
}
