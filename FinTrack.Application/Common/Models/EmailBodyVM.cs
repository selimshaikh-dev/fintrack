using FinTrack.Application.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Common.Models
{
    public class EmailBodyVM
    {
        public string CallBackUrl { get; set; }
        public string CallBackUrlForChangedPassword { get; set; }
        public string OldPassword { get; set; }
        public string EmailConfirmationToken { get; set;}
    }
}
