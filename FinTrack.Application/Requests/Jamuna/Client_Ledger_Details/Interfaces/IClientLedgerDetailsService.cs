using FinTrack.Application.Requests.Jamuna.Client_Ledger_Details.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.Client_Ledger_Details.Interfaces
{
    public interface IClientLedgerDetailsService : IDisposable
    {
        Task<DailyLedgerVM> GetClientLedgerDetails(string clientCode, DateTime startDate, DateTime endDate);
    }
}
