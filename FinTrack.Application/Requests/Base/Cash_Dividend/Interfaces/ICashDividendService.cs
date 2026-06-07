using FinTrack.Application.Requests.Base.Cash_Dividend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.Cash_Dividend.Interfaces
{
    public interface ICashDividendService
    {
        Task<List<CashDividendVM>> GetCashDividendInfo(string clientCode, DateTime endDate);
    }
}
