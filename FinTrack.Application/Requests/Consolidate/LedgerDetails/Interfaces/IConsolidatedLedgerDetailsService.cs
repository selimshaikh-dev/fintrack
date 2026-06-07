using FinTrack.Application.Requests.Jamuna.Client_Ledger_Details.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Consolidate.LedgerDetails.Interfaces
{
    public interface IConsolidatedLedgerDetailsService
    {
        Task<DailyLedgerVM> GetConsolidatedLedgerBalanceAsync(string memberID, DateTime startDate, DateTime endDate);
    }
}
