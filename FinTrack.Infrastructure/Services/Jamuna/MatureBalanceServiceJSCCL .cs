using Dapper;
using FinTrack.Application.Requests.Jamuna.MatureBalance.Interfaces;
using FinTrack.Application.Requests.Jamuna.MatureBalance.ViewModels;
using FinTrack.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Infrastructure.Services.Jamuna
{
    public class MatureBalanceServiceJSCCL : SqlDbContext<Mature_BalanceVM>, IMatureBalanceServiceJSCCL
    {
        public MatureBalanceServiceJSCCL(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<Mature_BalanceVM>> GetMatureBalanceJSCCLMulti(DateTime matureDate, DataTable dtClient)
        {
            string query = "Mature_Balance_JSCCL_v3";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Matured_Date", matureDate.Date, DbType.Date, ParameterDirection.Input);
            parameter.Add("@udt_Client", dtClient.AsTableValuedParameter("dbo.udt_client_bpid"));

            var data = await GetListBySPAsync(query, parameter);
            return data.ToList();
        }
    }
}
