using FinTrack.Application.Requests.Consolidate.InterestCalculation.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Consolidate.InterestCalculation.Interfaces
{
    public interface IDailyActivityBalanceServiceJSCCL
    {
        Task<List<Daily_Activity_BalVM>> GetDailyActivityBalanceJSCCL(DateTime startDate, DateTime endDate, DataTable dtClientBPID);
    }
}
