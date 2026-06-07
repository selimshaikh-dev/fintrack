using Dapper;
using FinTrack.Application.Requests.Base.MatureBalance.Interfaces;
using FinTrack.Application.Requests.Jamuna.MatureBalance.ViewModels;
using FinTrack.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Infrastructure.Services.Base
{
    public class MatureBalanceServiceGSL : SqlDbContextBase<Mature_BalanceVM>, IMatureBalanceServiceGSL
    {
        public MatureBalanceServiceGSL(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<Mature_BalanceVM>> GetMatureBalanceGSLMulti(DateTime matureDate, DataTable dtClient)
        {
            string query = "Mature_Balance_GSL_v4";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Matured_Date", matureDate.Date, DbType.Date, ParameterDirection.Input);
            parameter.Add("@udt_Client", dtClient.AsTableValuedParameter("dbo.udt_bpid_gsl"));

            var data = await GetListBySPAsync(query, parameter);
            return data.ToList();
        }
    }
}
