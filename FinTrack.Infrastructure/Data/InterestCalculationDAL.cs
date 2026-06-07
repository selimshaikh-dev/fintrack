using FinTrack.Application.Requests.Consolidate.InterestCalculation.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Infrastructure.Data
{
    public class InterestCalculationDAL : IInterestCalculationDAL
    {
        private readonly SqlConnection _con;
        private SqlTransaction _trans;

        public InterestCalculationDAL(IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("ConnectionStrings").GetSection("BaseConnection").Value;
            _con = new SqlConnection(connectionString);
        }

        private async Task ConnectionOpenAsync()
        {
            if (_con.State == ConnectionState.Closed)
                await _con.OpenAsync();
        }

        private async Task ConnectionClose()
        {
            if (_con.State == ConnectionState.Open)
               await _con.CloseAsync();
        }
        public async Task<DataTable> Client_BPID_Code_For_JSCCL(string clientCode)
        {
            await using var connection = _con;
            SqlDataAdapter da = null;
            DataTable dt_Client_BPID_Code_BPName = new DataTable();
            try
            {
                await ConnectionOpenAsync();
                using (SqlCommand command = new SqlCommand()
                {
                    Connection = connection,
                    CommandText = "Client_BPID_Code_BPName_For_JSCCL",
                    CommandType = CommandType.StoredProcedure,
                    CommandTimeout = 360
                })
                {
                    command.Parameters.Clear();
                    command.Parameters.Add("@clientCode", SqlDbType.NVarChar).Value = clientCode;
                    da = new SqlDataAdapter(command);
                    da.Fill(dt_Client_BPID_Code_BPName);
                    da.Dispose();
                }
            }
            catch (Exception ex)
            {
                await ConnectionClose();
                throw new Exception(ex.Message);
            }
            finally
            {
                await ConnectionClose();
            }
            return dt_Client_BPID_Code_BPName;
        }
    }
}
