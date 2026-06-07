using Dapper;
using FinTrack.Application.Requests.Jamuna.Client_Jamuna.Interfaces;
using FinTrack.Application.Requests.Jamuna.Client_Jamuna.ViewModels;
using FinTrack.Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Infrastructure.Services.Jamuna
{
    public class ClientServiceJamuna : SqlDbContext<ClientInfosJamunaVM>, IClientServiceJamuna
    {
        private readonly ApplicationDbContext _context;
        public ClientServiceJamuna(IConfiguration configuration, ApplicationDbContext context) : base(configuration)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<ClientInfosJamunaVM> GetClientInfosByEmail(string email)
        {
            string query = "Client_Infos_By_Email";

            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@email", email, DbType.String, ParameterDirection.Input);
            var data = await GetSingleBySPAsync(query, parameter);
            return data;
        }
        public async Task<ClientInfosJamunaVM> GetClientInfoInPlutoByEmail(string email)
        {
            string query = "Client_Infos_By_Email";

            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@email", email, DbType.String, ParameterDirection.Input);
            var data = await GetSingleBySPAsync(query, parameter);
            return data;
        }

        public async Task<ClientInfosJamunaVM> GetClientInfosJamuna(string clientCode)
        {
            string query = "Client_Infos_V2";

            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@code", clientCode, DbType.String, ParameterDirection.Input);
            var data = await GetSingleBySPAsync(query, parameter);
            return data;
        }
    }
}
