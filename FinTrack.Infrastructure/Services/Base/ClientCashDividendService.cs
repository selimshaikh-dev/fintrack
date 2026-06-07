using Dapper;
using FinTrack.Application.Requests.Base.Client_Dividend.Interfaces;
using FinTrack.Application.Requests.Base.Client_Dividend.ViewModels;
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
    public class ClientCashDividendService : SqlDbContextBase<CashDividendVM>, IClientCashDividendService, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public ClientCashDividendService(IConfiguration configuration, ApplicationDbContext context) : base(configuration)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<List<CashDividendVM>> GetClientCashDividend(string clientCode, int instrumentId)
        {
            var cashDividendList = new List<CashDividendVM>();

            try
            {
                if (string.IsNullOrWhiteSpace(clientCode))
                {
                    cashDividendList.Add(new CashDividendVM
                    {
                        Message = "Client code is empty"
                    });
                    return cashDividendList;
                }

                var ClientCode = clientCode.PadLeft(7, '0');

                var data = await GetCashDividendBySearch(ClientCode, instrumentId);
                if (data != null)
                {
                    cashDividendList = data.ToList();
                }
            }
            catch (Exception ex)
            {
                cashDividendList.Add(new CashDividendVM
                {
                    Message = "Error: " + ex.Message
                });
            }

            return cashDividendList;
        }

        public async Task<List<CashDividendVM>> GetCashDividendBySearch(string clientCode, int instrumentId)
        {

            string query = "sp_GetCashDividendReceipt";

            var parameters = new DynamicParameters();
            parameters.Add("@Client_Code", clientCode, DbType.String, ParameterDirection.Input);
            parameters.Add("@Instrument_ID", instrumentId, DbType.Int32, ParameterDirection.Input);

            var dividendList = await GetListBySPAsync(query, parameters);

            if (dividendList != null)
            {
                return dividendList.ToList();
            }
            else
            {
                return new List<CashDividendVM>();
            }
        }
    }
}