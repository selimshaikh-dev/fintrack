using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Infrastructure.Helpers
{
    public class AccountHelper
    {
        public string GetAccountStatus(bool isContinue, bool isDormant, bool isSuspended)
        {
            string accountStatus = string.Empty;
            if (isContinue == false)
            {
                accountStatus = "Closed";
            }
            else if (isContinue == true && isDormant == false)
            {
                accountStatus = "Active";
            }
            else
            {
                accountStatus = "Dormant";
            }

            if (isSuspended)
            {
                accountStatus = accountStatus + "[Suspended]";
            }
            return accountStatus;
        }
    }
}
