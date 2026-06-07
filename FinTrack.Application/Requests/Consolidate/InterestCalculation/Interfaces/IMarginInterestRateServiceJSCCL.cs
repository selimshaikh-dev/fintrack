using FinTrack.Application.Requests.Consolidate.InterestCalculation.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Consolidate.InterestCalculation.Interfaces
{
    public interface IMarginInterestRateServiceJSCCL
    {
        Task<List<Margin_Int_RateVM>> GetMarginInterestRateJSCCL(DateTime matureDate, DataTable dtClient);
    }
}
