using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.PortfolioAccountBalance.ViewModels
{
    public class PortfolioAccountBalanceVM
    {
        public Nullable<decimal> RealisedGL { get; set; }
        public Nullable<decimal> SecurityWithdraw { get; set; }
        public Nullable<decimal> SecurityDeposite { get; set; }
        public decimal TotalDeposite { get; set; }
        public decimal TotalWithdraw { get; set; }
        public decimal TotalCharges { get; set; }
        public decimal NetDeposite { get; set; }
    }
}
