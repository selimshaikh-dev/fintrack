using Dapper;
using FinTrack.Application.Requests.Base.Cash_Dividend.Interfaces;
using FinTrack.Application.Requests.Base.Cash_Dividend.ViewModels;
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
    public class CashDividendService : SqlDbContextBase<CashDividendVM>, ICashDividendService 
    {
        public CashDividendService(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<CashDividendVM>> GetCashDividendInfo(string clientCode, DateTime endDate)
        {
            string query = "SP_Report_GET_Cash_Dividend";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Report_Date", endDate.Date, DbType.Date, ParameterDirection.Input);
            parameter.Add("@Client_Code", clientCode, DbType.String, ParameterDirection.Input);
            var data = await GetListBySPAsync(query, parameter);
            return data.ToList();
        }
    }
}
