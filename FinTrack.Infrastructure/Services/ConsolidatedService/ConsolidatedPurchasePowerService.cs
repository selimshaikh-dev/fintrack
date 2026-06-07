using Dapper;
using FinTrack.Application.Requests.Consolidate.Purchase_Power.Interfaces;
using FinTrack.Application.Requests.Consolidate.Purchase_Power.ViewModels;
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
    public class ConsolidatedPurchasePowerService : SqlDbContextBase<PurchasePowerVM>, IConsolidatedPurchasePowerService
    {
        public ConsolidatedPurchasePowerService(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<PurchasePowerVM> GetConsolidatedPurchasePower(string ClientCode, DateTime TransactionDate)
        {
            string query = "GET_PURCHASE_POWER_CONSOLIDATED_V1";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@clientCode", ClientCode, DbType.String, ParameterDirection.Input);
            parameter.Add("@txnDate", TransactionDate.Date, DbType.Date, ParameterDirection.Input);
            parameter.Add("@companyId", 180, DbType.Int32, ParameterDirection.Input);
            var data = await GetSingleBySPAsync(query, parameter);
            return data;
        }
    }
}
