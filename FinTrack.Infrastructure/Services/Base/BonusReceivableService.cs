using Dapper;
using FinTrack.Application.Requests.Base.BonusReceivable.Interfaces;
using FinTrack.Application.Requests.Base.BonusReceivable.ViewModels;
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
    public class BonusReceivableService : SqlDbContextBase<BonusReceivableVM>, IBonusReceivableService
    {
        public BonusReceivableService(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<BonusReceivableVM>> GetBonusReceivableInfo(string clientCode, DateTime endDate)
        {
            string query = "Report_Bonus_Receivables";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Report_Date", endDate.Date, DbType.Date, ParameterDirection.Input);
            parameter.Add("@Client_Code", clientCode, DbType.String, ParameterDirection.Input);
            var data = await GetListBySPAsync(query, parameter);
            return data.ToList();
        }
    }
}
