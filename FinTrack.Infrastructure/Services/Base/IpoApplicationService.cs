using Dapper;
using FinTrack.Application.Requests.Base.Ipo.Interfaces;
using FinTrack.Application.Requests.Base.Ipo.ViewModels;
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
    public class IpoApplicationService : SqlDbContextBase<IpoApplicationVM>, IIpoApplicationService
    {
        public IpoApplicationService(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<List<IpoApplicationVM>> GetIpoApplicationInfo(int bpID)
        {
            string query = "GET_IPO_APPLICATION";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@BP_ID", bpID, DbType.Int32, ParameterDirection.Input);
            var data = await GetListBySPAsync(query, parameter);
            return data.ToList();
        }
    }
}
