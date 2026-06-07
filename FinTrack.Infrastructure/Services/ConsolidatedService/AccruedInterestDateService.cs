using Dapper;
using FinTrack.Application.Common.Models;
using FinTrack.Application.Requests.Consolidate.InterestCalculation.Interfaces;
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
    public class AccruedInterestDateService : SqlDbContextBase<DateTimeVM>, IAccruedInterestDateService
    {
        public AccruedInterestDateService(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<DateTime> GetInterestDateAsync(string clientCode, DateTime transactionDate)
        {
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@clientCode", clientCode, DbType.String, ParameterDirection.Input);
            parameter.Add("@maturedDate", transactionDate.Date, DbType.Date, ParameterDirection.Input);

            string query = "select dbo.GET_STARTDATE_ACCRUEDINTEREST_ClientCode(@clientCode, @maturedDate)";
            var date = await GetSingleDateTimeByQueryAsync(query, parameter);
            return date;
        }
    }
}
