using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.Cash_Dividend.ViewModels
{
    public class CashDividendVM
    {
        public string Instrument { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime Record_Date { get; set; }
        public System.DateTime Effective_Date { get; set; }
    }
}
