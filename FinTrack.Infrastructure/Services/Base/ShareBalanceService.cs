using Dapper;
using FinTrack.Application.Requests.Base.Share.Interfaces;
using FinTrack.Application.Requests.Base.Share.ViewModels;
using FinTrack.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace FinTrack.Infrastructure.Services.Base
{
    public class ShareBalanceService : SqlDbContextBase<ShareBalanceVM>, IShareBalanceService
    {
        public ShareBalanceService(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<ShareBalanceVM>> GetShareBalances(string clientCode, DateTime endDate)
        {
            string query = "Report_Protfolio_Share_Balance_V3";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Transaction_Date", endDate.Date, DbType.Date, ParameterDirection.Input);
            parameter.Add("@Client_Code", clientCode, DbType.String, ParameterDirection.Input);
            parameter.Add("@Company_ID", 180, DbType.Int32, ParameterDirection.Input);
            var data = await GetListBySPAsync(query, parameter);
            return data.ToList();
        }
    }
}
