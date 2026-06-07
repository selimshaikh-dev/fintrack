using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Consolidate.Portfolio.ViewModels
{
    public class FundStatusVM
    {
        public decimal PendingDeposite { get; set; }
        public decimal PendingWithdrawal { get; set; }
        public decimal FundAvailableToWithdrawal { get; set; }
        public decimal AccruedInterest { get; set; }
    }
}
