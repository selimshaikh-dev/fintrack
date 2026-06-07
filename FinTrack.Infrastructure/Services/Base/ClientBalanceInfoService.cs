using Dapper;
using FinTrack.Application.Requests.Base.Client_Dividend.Interfaces;
using FinTrack.Application.Requests.Base.Client_Dividend.ViewModels;
using FinTrack.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FinTrack.Infrastructure.Services.Base
{
    public class ClientBalanceInfoService : SqlDbContextBase<ClientBalanceInfoVM>, IClientBalanceInfoService, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public ClientBalanceInfoService(IConfiguration configuration, ApplicationDbContext context) : base(configuration)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<ClientBalanceInfoVM> GetClientBalanceInfo(string clientCode)
        {
            ClientBalanceInfoVM clientInfo = new ClientBalanceInfoVM();
            try
            {
                if (string.IsNullOrWhiteSpace(clientCode))
                {
                    clientInfo.Message = "Client code is empty.";
                    return clientInfo;
                }

                clientCode = clientCode.PadLeft(7, '0');
                DateTime today = DateTime.UtcNow;

                var balances = await GetClientLedgerBalance(clientCode, today);
                if (balances != null)
                {
                    clientInfo = balances.First();
                }
                else
                {
                    clientInfo.Message = "No information found.";
                }
            }
            catch (Exception ex)
            {
                clientInfo.Message = $"Something went wrong: {ex.Message}";
            }

            return clientInfo;
        }

        public async Task<List<ClientBalanceInfoVM>> GetClientLedgerBalance(string clientCode, DateTime transactionDate)
        {
            string query = "Get_Client_Balance_Info";

            var parameters = new DynamicParameters();
            parameters.Add("@Client_Code", clientCode, DbType.String, ParameterDirection.Input);
            parameters.Add("@TransactionDate", transactionDate.Date, DbType.Date, ParameterDirection.Input);

            var BalanceInfo = await GetListBySPAsync(query, parameters);

            if (BalanceInfo != null)
            {
                return BalanceInfo.ToList();
            }
            else
            {
                return new List<ClientBalanceInfoVM>();
            }   
        }
    }
}