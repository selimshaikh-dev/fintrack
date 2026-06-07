using Dapper;
using FinTrack.Application.Requests.Base.Client_Base.Interfaces;
using FinTrack.Application.Requests.Base.Client_Base.ViewModels;
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
    public class ClientBaseService : SqlDbContextBase<Client_InfosVM>, IClientBaseService
    {
        private readonly ApplicationDbContext _context;
        public ClientBaseService(IConfiguration configuration, ApplicationDbContext context) : base(configuration)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<Client_InfosVM> GetClientInfos(string clientCode)
        {
            string query = "Client_Infos";

            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Client_Code", clientCode, DbType.String, ParameterDirection.Input);
            parameter.Add("@Comapany_Id", 180, DbType.Int32, ParameterDirection.Input);
            var data = await GetSingleBySPAsync(query, parameter);
            return data;
        }
    }
}
