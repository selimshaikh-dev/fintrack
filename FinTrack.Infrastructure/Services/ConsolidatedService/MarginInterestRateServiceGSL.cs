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
    public class MarginInterestRateServiceGSL : SqlDbContextBase<Margin_Int_RateVM>, IMarginInterestRateServiceGSL
    {
        public MarginInterestRateServiceGSL(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<Margin_Int_RateVM>> GetMarginInterestRateGSL(DateTime matureDate, DataTable dtClient)
        {
            string query = "Margin_Int_Rate_GSL";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@mature_date", matureDate.Date, DbType.Date, ParameterDirection.Input);
            parameter.Add("@udt_Client", dtClient.AsTableValuedParameter("dbo.udt_bpid_gsl"));

            var data = await GetListBySPAsync(query, parameter);
            return data.ToList();
        }
    }
}
