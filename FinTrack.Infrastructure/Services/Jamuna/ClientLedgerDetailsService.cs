using AutoMapper.Execution;
using Dapper;
using FinTrack.Application.Common.Interfaces;
using FinTrack.Application.Requests.Base.Client_Base.Interfaces;
using FinTrack.Application.Requests.Base.Client_Base.ViewModels;
using FinTrack.Application.Requests.Consolidate.Portfolio.ViewModels;
using FinTrack.Application.Requests.Jamuna.Client_Jamuna.Interfaces;
using FinTrack.Application.Requests.Jamuna.Client_Jamuna.ViewModels;
using FinTrack.Application.Requests.Jamuna.Client_Ledger_Details.Interfaces;
using FinTrack.Application.Requests.Jamuna.Client_Ledger_Details.ViewModels;
using FinTrack.Infrastructure.Data;
using FinTrack.Infrastructure.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FinTrack.Infrastructure.Services.Jamuna
{
    public class ClientLedgerDetailsService : SqlDbContext<LedgerListVM>, IClientLedgerDetailsService
    {
        private readonly ApplicationDbContext _context;
        private readonly IClientServiceJamuna _clientServiceJamuna;
        private readonly IClientBaseService _clientBaseService;
        public ClientLedgerDetailsService(IConfiguration configuration, ApplicationDbContext context, IClientServiceJamuna clientService, IClientBaseService clientBaseService) : base(configuration)
        {
            _context = context;
            _clientServiceJamuna = clientService;
            _clientBaseService = clientBaseService;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<DailyLedgerVM> GetClientLedgerDetails(string memberId, DateTime startDate, DateTime endDate)
        {
            DailyLedgerVM objDailyLedgerDetails = new DailyLedgerVM();
            objDailyLedgerDetails.ClientDetails = new ClientDetailsVM();
            objDailyLedgerDetails.LedgerDetails = new List<LedgerListVM>();
            objDailyLedgerDetails.Message = string.Empty;
            Client_InfosVM clientInfosBase = new Client_InfosVM();
            ClientInfosJamunaVM clientInfosJamuna = new ClientInfosJamunaVM();
            AccountHelper accountHelper = new AccountHelper();

            try
            {
                if (memberId.Contains("g") || memberId.Contains("G"))
                {
                    memberId = memberId.Contains("g") ? memberId.TrimStart('g') : memberId.Contains("G") ? memberId.TrimStart('G') : memberId;
                    memberId = memberId.PadLeft(7, '0');
                    clientInfosBase = await _clientBaseService.GetClientInfos(memberId);
                    if (clientInfosBase != null)
                    {
                        clientInfosJamuna = await _clientServiceJamuna.GetClientInfosJamuna(memberId);
                        if (clientInfosJamuna == null)
                        {
                            objDailyLedgerDetails.Message = "Member information is not found!";
                            return objDailyLedgerDetails;
                        }

                        if (string.IsNullOrEmpty(clientInfosJamuna.JamunaMemberID))
                        {
                            objDailyLedgerDetails.Message = "Member ID is not found!";
                            return objDailyLedgerDetails;
                        }
                    }
                    else
                    {
                        objDailyLedgerDetails.Message = "Member information is not found!";
                        return objDailyLedgerDetails;
                    }
                }
                else
                {
                    memberId = memberId.PadLeft(6, '0').PadLeft(8, '7');
                    clientInfosJamuna = await _clientServiceJamuna.GetClientInfosJamuna(memberId);
                    if (clientInfosJamuna != null)
                    {
                        if (!string.IsNullOrEmpty(clientInfosJamuna.ClientCode))
                        {
                            clientInfosBase = await _clientBaseService.GetClientInfos(clientInfosJamuna.ClientCode.PadLeft(7, '0'));
                            if (clientInfosBase == null)
                            {
                                objDailyLedgerDetails.Message = "Client information is not found!";
                                return objDailyLedgerDetails;
                            }
                        }
                        else
                        {
                            objDailyLedgerDetails.Message = $"Member Id {memberId} is not a client of gsl!";
                            return objDailyLedgerDetails;
                        }
                    }
                    else
                    {
                        objDailyLedgerDetails.Message = "Member information is not found!";
                        return objDailyLedgerDetails;
                    }
                }

                //Client Details Section
                objDailyLedgerDetails.ClientDetails.Name = clientInfosBase.BP_Name;
                objDailyLedgerDetails.ClientDetails.Code = clientInfosBase.Client_Code.TrimStart('0');
                objDailyLedgerDetails.ClientDetails.JamunaMemberID = clientInfosJamuna.JamunaMemberCode.TrimStart('7').TrimStart('0');
                objDailyLedgerDetails.ClientDetails.AsOn = endDate.ToString("dd-MMMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                objDailyLedgerDetails.ClientDetails.Type_Name_Marketing = clientInfosJamuna.Type_Name_Marketing; ;
                objDailyLedgerDetails.ClientDetails.AccountStatus = accountHelper.GetAccountStatus(clientInfosBase.Is_Continued, clientInfosBase.Is_Dormant, clientInfosBase.IS_Suspended);
                objDailyLedgerDetails.ClientDetails.LastTranDate = "From " + startDate.ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture) + " To " + endDate.ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);

                var data = await GetLedgerBalanceList(clientInfosJamuna.JamunaMemberCode, startDate,endDate);
                objDailyLedgerDetails.LedgerDetails = data.OrderBy(x => x.Transaction_Date).ThenBy(x => x.Description).ThenBy(x => x.Quantity).ThenBy(x => x.Rate).ToList();
            }
            catch (Exception ex)
            {
                objDailyLedgerDetails.Message = $"Something went wrong- " + ex.Message;
                return objDailyLedgerDetails;
            }

            return objDailyLedgerDetails;
        }
        public async Task<List<LedgerListVM>> GetLedgerBalanceList(string memberID, DateTime startDate, DateTime endDate)
        {
            string query = "Report_Transaction_Wise_Client_Ledger";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Member_ID", memberID, DbType.String, ParameterDirection.Input);
            parameter.Add("@Start_Date", startDate.Date, DbType.Date, ParameterDirection.Input);
            parameter.Add("@End_Date", endDate.Date, DbType.Date, ParameterDirection.Input);

            var data = await GetListBySPAsync(query, parameter);
            return data.ToList();
        }
    }
}
