using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Domain.Enums
{
    public enum ViewTypeEnum
    {
        [Description("Server")]
        Server = 1,
        [Description("Client")]
        Client = 2,
        [Description("Server & Client")]
        Both = 3
    }
}
