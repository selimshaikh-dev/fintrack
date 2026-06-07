using Dapper;
using FinTrack.Application.Requests.Base.PortfolioAccountBalance.Interfaces;
using FinTrack.Application.Requests.Base.PortfolioAccountBalance.ViewModels;
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
    public class PortfolioAccountBalanceService : SqlDbContextBase<PortfolioAccountBalanceVM>, IPortfolioAccountBalanceService
    {
        public PortfolioAccountBalanceService(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<PortfolioAccountBalanceVM> GetPortfolioAccountBalance(string ClientCode, DateTime StartDate, DateTime EndDate)
        {
            string query = "Report_Portfolio_Account_Balance_Globe_Jamuna";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Client_Code", ClientCode, DbType.String, ParameterDirection.Input);
            parameter.Add("@Start_Date", StartDate.Date, DbType.Date, ParameterDirection.Input);
            parameter.Add("@End_Date", EndDate.Date, DbType.Date, ParameterDirection.Input);
            parameter.Add("@Company_ID", 180, DbType.Int32, ParameterDirection.Input);
            var data = await GetSingleBySPAsync(query, parameter);
            return data;
        }
    }
}
