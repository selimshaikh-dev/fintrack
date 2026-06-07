using FinTrack.Application.Requests.Base.PortfolioAccountBalance.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.PortfolioAccountBalance.Interfaces
{
    public interface IPortfolioAccountBalanceService
    {
        Task<PortfolioAccountBalanceVM> GetPortfolioAccountBalance(string ClientCode, DateTime StartDate, DateTime EndDate);
    }
}
