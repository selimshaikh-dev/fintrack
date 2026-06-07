using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Consolidate.Portfolio.ViewModels
{
    public class MarginRiskStatusVM
    {
        public decimal Current_LTV { get; set; }
        public decimal Authorized_LTV { get; set; }
        public string Risk_Status { get; set; }
        public decimal Depo_Buy_Req { get; set; }
        public decimal Adjust_Req { get; set; }
        public decimal Sell_Req { get; set; }
    }
}
