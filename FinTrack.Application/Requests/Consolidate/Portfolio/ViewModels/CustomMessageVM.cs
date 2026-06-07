using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Consolidate.Portfolio.ViewModels
{
    public class CustomMessageVM
    {
        public string CustomMessageHeader { get; set; }
        public string CustomMessageBody { get; set; }
        public string ColorCode { get; set; }
        public string FontColorCode { get; set; }
    }
}
