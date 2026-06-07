using Dapper;
using FinTrack.Application.Requests.Consolidate.InterestCalculation.Interfaces;
using FinTrack.Application.Requests.Consolidate.InterestCalculation.ViewModels;
using FinTrack.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FinTrack.Infrastructure.Services.ConsolidatedService
{
    public class DailyActivityBalanceServiceJSCCL : SqlDbContext<Daily_Activity_BalVM>, IDailyActivityBalanceServiceJSCCL
    {
        public DailyActivityBalanceServiceJSCCL(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<Daily_Activity_BalVM>> GetDailyActivityBalanceJSCCL(DateTime startDate, DateTime endDate, DataTable dtClientBPID)
        {
            string query = "Daily_Activity_Bal_JSCCL";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@startDate", startDate.Date, DbType.Date, ParameterDirection.Input);
            parameter.Add("@endDate", startDate.Date, DbType.Date, ParameterDirection.Input);
            parameter.Add("@udt_Client", dtClientBPID.AsTableValuedParameter("dbo.udt_client_bpid"));

            var data = await GetListBySPAsync(query, parameter);
            return data.ToList();
        }
    }
}
