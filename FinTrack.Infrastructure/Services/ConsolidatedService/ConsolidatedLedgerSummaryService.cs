using Dapper;
using FinTrack.Application.Requests.Base.Client_Base.Interfaces;
using FinTrack.Application.Requests.Base.Client_Base.ViewModels;
using FinTrack.Application.Requests.Consolidate.LedgerSummary.Interfaces;
using FinTrack.Application.Requests.Consolidate.Portfolio.ViewModels;
using FinTrack.Application.Requests.Jamuna.Client_Jamuna.Interfaces;
using FinTrack.Application.Requests.Jamuna.Client_Jamuna.ViewModels;
using FinTrack.Application.Requests.Jamuna.Client_Ledger_Details.ViewModels;
using FinTrack.Infrastructure.Data;
using FinTrack.Infrastructure.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Infrastructure.Services.ConsolidatedService
{
    public class ConsolidatedLedgerSummaryService : SqlDbContextBase<LedgerListVM>, IConsolidatedLedgerSummaryService
    {
        private readonly IClientBaseService _clientBaseService;
        private readonly IClientServiceJamuna _clientServiceJamuna;
        public ConsolidatedLedgerSummaryService(IConfiguration configuration, IClientBaseService clientBaseService, IClientServiceJamuna clientServiceJamuna) : base(configuration)
        {
            _clientBaseService = clientBaseService;
            _clientServiceJamuna = clientServiceJamuna;
        }

        public async Task<DailyLedgerVM> GetConsolidatedLedgerSummaryAsync(string memberId, DateTime startDate, DateTime endDate)
        {
            DailyLedgerVM objDailyLedgerDetails = new DailyLedgerVM();
            objDailyLedgerDetails.LedgerDetails = new List<LedgerListVM>();
            objDailyLedgerDetails.ClientDetails = new ClientDetailsVM();
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
                        if (clientInfosBase.Client_Group_ID != 5)
                        {
                            objDailyLedgerDetails.Message = $"Client Code {clientInfosBase.Client_Code} is not Jamuna Member!";
                            return objDailyLedgerDetails;
                        }
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
                            if (clientInfosBase.Client_Group_ID != 5)
                            {
                                objDailyLedgerDetails.Message = $"Member Id {memberId} is not Jamuna Member!";
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

                var data = await GetCombinedLedgerSummary(clientInfosBase.Client_Code, startDate, endDate);
                var prevDate = startDate.AddDays(-1);
                var balance = data.Where(x => x.Transaction_Date.Date == prevDate.Date).Sum(x => x.Balance);

                LedgerListVM objLedger = new LedgerListVM();
                objLedger.Transaction_Date = startDate.AddDays(-1);
                objLedger.Particulars = "GSL+JSCCL";
                objLedger.Description = "Combined Closing Balance";
                objLedger.Quantity = 0;
                objLedger.Rate = 0;
                objLedger.Debit = 0;
                objLedger.Credit = 0;
                objLedger.Commission = 0;
                objLedger.Balance = balance;
                data.Add(objLedger);
                objDailyLedgerDetails.LedgerDetails = data.Where(x => x.Particulars != "GSL" && x.Particulars != "JSCCL").OrderBy(x => x.Transaction_Date).ToList();
                return objDailyLedgerDetails;
            }
            catch (Exception ex)
            {
                objDailyLedgerDetails.Message = ex.Message;
                return objDailyLedgerDetails;
            }
        }
        public async Task<List<LedgerListVM>> GetCombinedLedgerSummary(string memberID, DateTime startDate, DateTime endDate)
        {
            string query = "Report_Combined_Ledger_Summary";
            DynamicParameters parameter = new DynamicParameters();
            parameter.Add("@Client_Code", memberID, DbType.String, ParameterDirection.Input);
            parameter.Add("@Start_Date", startDate.Date, DbType.Date, ParameterDirection.Input);
            parameter.Add("@End_Date", endDate.Date, DbType.Date, ParameterDirection.Input);

            var data = await GetListBySPAsync(query, parameter);
            return data.ToList();
        }
    }
}
