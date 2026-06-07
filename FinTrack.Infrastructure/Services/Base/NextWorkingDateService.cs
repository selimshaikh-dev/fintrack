using Dapper;
using FinTrack.Application.Common.Interfaces;
using FinTrack.Application.Common.Models;
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
    public class NextWorkingDateService : SqlDbContextBase<DateTimeVM>, INextWorkingDateService
    {
        private readonly ApplicationDbContext _context;
        public NextWorkingDateService(IConfiguration configuration, ApplicationDbContext context) : base(configuration)
        {
            _context = context;
        }
        public async Task<DateTime> GetNextWorkingDate(DateTime txnDate)
        {
            string query = "SP_GET_NEXT_WORKING_DATE";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@txn_date", txnDate.Date, DbType.Date, ParameterDirection.Input);
            parameter.Add("@next_working_date", null, dbType: DbType.Date, direction: ParameterDirection.Output, 100);
            
            var data = await GetSingleDateTimeBySPAsync(query, parameter);
            var nextWorkingDate = parameter.Get<DateTime>("@next_working_date");
            return nextWorkingDate;
        }
    }
}
