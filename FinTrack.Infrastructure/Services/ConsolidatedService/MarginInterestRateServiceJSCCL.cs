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
    public class MarginInterestRateServiceJSCCL : SqlDbContext<Margin_Int_RateVM>, IMarginInterestRateServiceJSCCL
    {
        public MarginInterestRateServiceJSCCL(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<Margin_Int_RateVM>> GetMarginInterestRateJSCCL(DateTime matureDate, DataTable dtClient)
        {
            string query = "Margin_Int_Rate_JSCCL";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@mature_date", matureDate.Date, DbType.Date, ParameterDirection.Input);
            parameter.Add("@udt_Client", dtClient.AsTableValuedParameter("dbo.udt_client_bpid"));

            var data = await GetListBySPAsync(query, parameter);
            return data.ToList();
        }
    }
}
