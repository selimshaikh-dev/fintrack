using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Domain.Enums
{
    public enum UsersRole
    {
        [Description("Developer")]
        Developer = 1,
        [Description("Super Admin")]
        SuperAdmin = 2,
        [Description("Admin")]
        Admin = 3,
        [Description("General User")]
        GUser = 4,
        [Description("Guest")]
        Guest = 5,
        [Description("Member")]
        Member = 6
    }
}
