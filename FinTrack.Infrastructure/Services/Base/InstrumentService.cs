using Amazon.SimpleEmail.Model;
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
    public class InstrumentService : SqlDbContextBase<InstrumentVM>, IInstrumentService, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public InstrumentService(IConfiguration configuration, ApplicationDbContext context) : base(configuration)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<List<InstrumentVM>> GetAllInstrument()
        {
            var instruments = new List<InstrumentVM>();

            try
            {
                var data = await GetInstrumentList();
                if (data != null)
                {
                    instruments = data.ToList();
                }
            }
            catch (Exception ex)
            {
                instruments.Add(new InstrumentVM
                {
                    Message = "Error: " + ex.Message
                });
            }

            return instruments;
        }

        private async Task<IEnumerable<InstrumentVM>> GetInstrumentList()
        {
            const string query = "SELECT Instrument_ID, Instrument_ID_DSE FROM Instrument";

            try
            {
                var parameters = new DynamicParameters();
                var result = await GetListByQueryAsync(query, parameters);
                return result;
            }
            catch
            {
                return new List<InstrumentVM>();
            }
        }

        public async Task<bool> CheckInterimDividend(int instrumentId)
        {
            if (instrumentId <= 0) 
             return false;

            try
            {
                string query = "GET_Bond_Type_Instrument";
                var parameters = new DynamicParameters();
                parameters.Add("@InstrumentID", instrumentId, DbType.Int32, ParameterDirection.Input);

                var InstrumentName = await GetSingleStringBySPAsync(query, parameters);

                if (!string.IsNullOrEmpty(InstrumentName))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch 
            {
                return false;
            }
        }
    }
}