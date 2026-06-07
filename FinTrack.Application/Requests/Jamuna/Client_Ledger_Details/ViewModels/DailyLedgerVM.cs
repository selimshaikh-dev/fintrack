using FinTrack.Application.Requests.Consolidate.Portfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.Client_Ledger_Details.ViewModels
{
    public class DailyLedgerVM
    {
        public string Message { get; set; }
        public string Params { get; set; }
        public ClientDetailsVM ClientDetails { get; set; }
        public List<LedgerListVM> LedgerDetails { get; set; }
    }
}
