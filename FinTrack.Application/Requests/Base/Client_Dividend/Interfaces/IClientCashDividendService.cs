using FinTrack.Application.Requests.Base.Client_Dividend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.Client_Dividend.Interfaces
{
    public interface IClientCashDividendService : IDisposable
    {
        Task<List<CashDividendVM>> GetClientCashDividend(string clientCode, int instrumentId);
    }
}