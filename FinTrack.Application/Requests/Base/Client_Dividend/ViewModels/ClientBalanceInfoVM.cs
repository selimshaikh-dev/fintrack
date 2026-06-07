using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.Client_Dividend.ViewModels
{
    public class ClientBalanceInfoVM
    {
        public string Client_Code { get; set; }
        public string Client_Name { get; set; }
        public string BO_ID_DSE { get; set; }
        public decimal Ledger_Balance { get; set; }
        public decimal Mature_Balance { get; set; }
        public string Message { get; set; }
    }
}