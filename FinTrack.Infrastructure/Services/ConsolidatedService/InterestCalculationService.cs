using FinTrack.Application.Common.Interfaces;
using FinTrack.Application.Requests.Base.MatureBalance.Interfaces;
using FinTrack.Application.Requests.Consolidate.InterestCalculation.Interfaces;
using FinTrack.Application.Requests.Consolidate.InterestCalculation.ViewModels;
using FinTrack.Application.Requests.Jamuna.MatureBalance.Interfaces;
using FinTrack.Application.Requests.Jamuna.MatureBalance.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Infrastructure.Services.ConsolidatedService
{
    public class InterestCalculationService : IInterestCalculationService
    {
        private readonly INextWorkingDateService _workingDateService;
        private readonly IAccruedInterestDateService _accruedInterestDateService;
        private readonly IInterestCalculationDAL _interestCalculationDAL;
        private readonly IDailyActivityBalanceServiceJSCCL _dailyActivityBalanceServiceJSCCL;
        private readonly IDailyActivityBalanceServiceGSL _dailyActivityBalanceServiceGSL;
        private readonly IMatureBalanceServiceJSCCL _matureBalanceServiceJSCCL;
        private readonly IMarginInterestRateServiceJSCCL _marginInterestRateService;
        private readonly IMatureBalanceServiceGSL _marginInterestRateServiceGSL;
        private readonly IMarginInterestRateServiceGSL _marginInterestRateServiceJGSL;
        public InterestCalculationService(INextWorkingDateService workingDateService, IAccruedInterestDateService accruedInterestDateService, IInterestCalculationDAL interestCalculationDAL, IDailyActivityBalanceServiceJSCCL dailyActivityBalanceServiceJSCCL, IMatureBalanceServiceJSCCL matureBalanceServiceJSCCL, IMarginInterestRateServiceJSCCL marginInterestRateService, IDailyActivityBalanceServiceGSL dailyActivityBalanceServiceGSL, IMatureBalanceServiceGSL marginInterestRateServiceGSL, IMarginInterestRateServiceGSL marginInterestRateServiceJGSL)
        {
            _workingDateService = workingDateService;
            _accruedInterestDateService = accruedInterestDateService;
            _interestCalculationDAL = interestCalculationDAL;
            _dailyActivityBalanceServiceJSCCL = dailyActivityBalanceServiceJSCCL;
            _matureBalanceServiceJSCCL = matureBalanceServiceJSCCL;
            _marginInterestRateService = marginInterestRateService;
            _dailyActivityBalanceServiceGSL = dailyActivityBalanceServiceGSL;
            _marginInterestRateServiceGSL = marginInterestRateServiceGSL;
            _marginInterestRateServiceJGSL = marginInterestRateServiceJGSL;
        }
        public async Task<decimal> GetInterest(string clientCode, DateTime txnDate, int clientType, int bpIdgsl)
        {
            var nextWorkingDate = await _workingDateService.GetNextWorkingDate(txnDate);
            var interestDate = await _accruedInterestDateService.GetInterestDateAsync(clientCode, nextWorkingDate);
            var accrued_Interest = await Get_Accrued_Interest_GSL_JSCCL_Single_v2(interestDate, nextWorkingDate, clientCode, clientType, bpIdgsl);
            return accrued_Interest;
        }
        public async Task<decimal> Get_Accrued_Interest_GSL_JSCCL_Single_v2(DateTime matStartDate, DateTime matEndDate, string clientCode, int client_type, int bpIdgsl) // Pass Client Type ID as parameter
        {
            List<Client_BPID_InterestAmount> client_bpid_interestAmount_List = new List<Client_BPID_InterestAmount>();
            DataTable dt_client_BPID_Code_BPName = new DataTable();
            DataTable dt_client_bpid = new DataTable();
            decimal interestAmount = 0.00m;

            if (client_type == 1) // Type 1 = ‘Not Closed’ in both GSL & JSCCL, linked with JSCCL
            {
                dt_client_BPID_Code_BPName = await _interestCalculationDAL.Client_BPID_Code_For_JSCCL(clientCode);
                dt_client_bpid = new DataView(dt_client_BPID_Code_BPName).ToTable(false, "BP_ID");
                client_bpid_interestAmount_List = await GetInterestJSCCL(matStartDate, matEndDate, dt_client_bpid);
                interestAmount = client_bpid_interestAmount_List.Select(x => x.Interest_Amount).FirstOrDefault();
            }
            else if (client_type == 3)// Type 3 = GSL Margin client has no link with JSCCL
            {
                dt_client_bpid.Columns.Add(new DataColumn("BP_ID", typeof(int)));
                dt_client_bpid.Rows.Add(bpIdgsl);
                client_bpid_interestAmount_List = await GetInterestGSL(matStartDate, matEndDate, dt_client_bpid);
                interestAmount = client_bpid_interestAmount_List.Select(x => x.Interest_Amount).FirstOrDefault();
            }
            else {
                interestAmount = 0m;
            }

            return interestAmount;

        }
        public async Task<List<Client_BPID_InterestAmount>> GetInterestJSCCL(DateTime startDate, DateTime endDate, DataTable dtClientBPID)
        {
            DataTable client_BPID_ClientCode_BPName = new DataTable();
            DataTable dt_Client_Margin_Interest_Info = new DataTable();
            List<Daily_Activity_BalVM> daywiseActivityBalanceList = new List<Daily_Activity_BalVM>();
            List<Mature_BalanceVM> lastDay_MatureBalance_List = new List<Mature_BalanceVM>(); // This is for the Opening Balance
            List<Margin_Int_RateVM> margin_Int_Rate_List = new List<Margin_Int_RateVM>();
            List<Client_BPID_InterestAmount> client_BPID_InterestAmount_List = new List<Client_BPID_InterestAmount>();
            try
            {
                daywiseActivityBalanceList = await _dailyActivityBalanceServiceJSCCL.GetDailyActivityBalanceJSCCL(startDate, endDate, dtClientBPID);
                lastDay_MatureBalance_List = await _matureBalanceServiceJSCCL.GetMatureBalanceJSCCLMulti(startDate.AddDays(-1), dtClientBPID);
                margin_Int_Rate_List = await _marginInterestRateService.GetMarginInterestRateJSCCL(startDate, dtClientBPID);

                var date_difference = (endDate - startDate).Days + 1;
                foreach (DataRow client in dtClientBPID.Rows)
                {
                    decimal lastDay_MatureBalance = 0.00m;
                    decimal interest_rate = 0.00m;
                    decimal interestAmount_daily = 0.00m;

                    var single_client_daywiseActivityBalanceList = daywiseActivityBalanceList.Where(x => x.Client_BP_ID == Convert.ToInt32(client["BP_ID"])).ToList();
                    lastDay_MatureBalance = lastDay_MatureBalance_List.Where(x => x.Client_BP_ID == Convert.ToInt32(client["BP_ID"])).Select(x => x.Mature_Balance).FirstOrDefault();
                    interest_rate = margin_Int_Rate_List.Where(x => x.Client_BP_ID == Convert.ToInt32(client["BP_ID"])).Select(x => x.Interest_Rate).FirstOrDefault();

                    while (startDate <= endDate)
                    {
                        var daywise_Activity_Balance = single_client_daywiseActivityBalanceList.Where(x => x.Mature_Date == startDate.Date).Select(x => x.Amount).FirstOrDefault();

                        if (daywise_Activity_Balance != 0)
                        {
                            lastDay_MatureBalance = lastDay_MatureBalance + daywise_Activity_Balance;
                        }

                        if (lastDay_MatureBalance < 0)
                        {
                            interestAmount_daily = interestAmount_daily + Math.Round(((lastDay_MatureBalance * (interest_rate / 100)) / 360) * -1, 2);
                        }
                        startDate = startDate.AddDays(1);
                    }

                    var interestAmount = interestAmount_daily;

                    Client_BPID_InterestAmount obj_client_BPID_InterestAmount = new Client_BPID_InterestAmount { BP_ID = Convert.ToInt32(client["BP_ID"]), Interest_Amount = Convert.ToDecimal(String.Format("{0:n}", interestAmount)) };

                    client_BPID_InterestAmount_List.Add(obj_client_BPID_InterestAmount);

                    startDate = startDate.AddDays(-date_difference);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return client_BPID_InterestAmount_List;
        }
        public async Task<List<Client_BPID_InterestAmount>> GetInterestGSL(DateTime startDate, DateTime endDate, DataTable dtClientBPID)
        {
            DataTable dt_Client_BPID_Interest = new DataTable();
            List<Daily_Activity_BalVM> daywiseActivityBalanceList = new List<Daily_Activity_BalVM>();
            List<Mature_BalanceVM> lastDay_MatureBalance_List = new List<Mature_BalanceVM>(); // This is for the Opening Balance
            List<Margin_Int_RateVM> margin_Int_Rate_List = new List<Margin_Int_RateVM>();
            List<Client_BPID_InterestAmount> client_BPID_InterestAmount_List = new List<Client_BPID_InterestAmount>(); // All Client's Info to show in Grid

            try
            {
                daywiseActivityBalanceList = await _dailyActivityBalanceServiceGSL.GetDailyActivityBalanceGSL(startDate, endDate, dtClientBPID);
                lastDay_MatureBalance_List = await _marginInterestRateServiceGSL.GetMatureBalanceGSLMulti(startDate.AddDays(-1), dtClientBPID);
                margin_Int_Rate_List = await _marginInterestRateServiceJGSL.GetMarginInterestRateGSL(startDate, dtClientBPID);
                var date_difference = (endDate - startDate).Days + 1;

                foreach (DataRow client in dtClientBPID.Rows)
                {
                    decimal lastDay_MatureBalance = 0.00m;
                    decimal interest_rate = 0.00m;
                    decimal interestAmount_daily = 0.00m;

                    var single_client_daywiseActivityBalanceList = daywiseActivityBalanceList.Where(x => x.Client_BP_ID == Convert.ToInt32(client["BP_ID"])).ToList();
                    lastDay_MatureBalance = lastDay_MatureBalance_List.Where(x => x.Client_BP_ID == Convert.ToInt32(client["BP_ID"])).Select(x => x.Mature_Balance).FirstOrDefault();
                    interest_rate = margin_Int_Rate_List.Where(x => x.Client_BP_ID == Convert.ToInt32(client["BP_ID"])).Select(x => x.Interest_Rate).FirstOrDefault();

                    while (startDate <= endDate)
                    {
                        var daywise_Activity_Balance = single_client_daywiseActivityBalanceList.Where(x => x.Mature_Date == startDate.Date).Select(x => x.Amount).FirstOrDefault();

                        if (daywise_Activity_Balance != 0)
                        {
                            lastDay_MatureBalance = lastDay_MatureBalance + daywise_Activity_Balance;
                        }

                        if (lastDay_MatureBalance < 0)
                        {
                            interestAmount_daily = interestAmount_daily + Math.Round(((lastDay_MatureBalance * (interest_rate / 100)) / 360) * -1, 2);
                        }
                        startDate = startDate.AddDays(1);
                    }

                    var interestAmount = interestAmount_daily;

                    Client_BPID_InterestAmount obj_client_BPID_InterestAmount = new Client_BPID_InterestAmount { BP_ID = Convert.ToInt32(client["BP_ID"]), Interest_Amount = Convert.ToDecimal(String.Format("{0:n}", interestAmount)) };

                    client_BPID_InterestAmount_List.Add(obj_client_BPID_InterestAmount);

                    startDate = startDate.AddDays(-date_difference);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return client_BPID_InterestAmount_List;
        }
    }
}
