using Dapper;
using FinTrack.Application.Requests.Consolidate.InterestCalculation.Interfaces;
using FinTrack.Application.Requests.Consolidate.InterestCalculation.ViewModels;
using FinTrack.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Infrastructure.Services.ConsolidatedService
{
    public class DailyActivityBalanceServiceGSL : SqlDbContextBase<Daily_Activity_BalVM>, IDailyActivityBalanceServiceGSL
    {
        public DailyActivityBalanceServiceGSL(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<Daily_Activity_BalVM>> GetDailyActivityBalanceGSL(DateTime startDate, DateTime endDate, DataTable dtClientBPID)
        {
            string query = "Daily_Activity_Bal_GSL_using_Effective_Maturity";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@startDate", startDate.Date, DbType.Date, ParameterDirection.Input);
            parameter.Add("@endDate", startDate.Date, DbType.Date, ParameterDirection.Input);
            parameter.Add("@udt_Client", dtClientBPID.AsTableValuedParameter("dbo.udt_bpid_gsl"));

            var data = await GetListBySPAsync(query, parameter);
            return data.ToList();
        }
    }
}
