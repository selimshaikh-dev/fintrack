using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Consolidate.Portfolio.ViewModels
{
    public class MarginStatusVM
    {
        public string LoanTypeDescription { get; set; }
        public decimal MML { get; set; }
        public decimal EML { get; set; }
        public decimal Authorized_LTV { get; set; }
        public decimal Current_LTV { get; set; }
        public decimal LoanOverUsage { get; set; }
        public decimal LoanOverUsageStockValue { get; set; }
        public decimal MarginCallStockValue { get; set; }
        public decimal LiquidationStockValue { get; set; }
        public decimal PenWarLTV {  get; set; }
        public decimal MarCalLTV { get; set; }
        public decimal LiqLTV { get; set; }

    }
}
