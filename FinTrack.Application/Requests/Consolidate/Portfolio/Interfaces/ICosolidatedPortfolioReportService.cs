using FinTrack.Application.Requests.Consolidate.Portfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Consolidate.Portfolio.Interfaces
{
    public interface ICosolidatedPortfolioReportService
    {
        Task<CosolidatedPortfolioVM> GetCosolidatedPortfolioReport(string memberId, DateTime endDate);       
    }
}
