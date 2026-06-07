using Amazon.SimpleEmail.Model;
using AutoMapper.Execution;
using Dapper;
using FinTrack.Application.Common.Constants;
using FinTrack.Application.Common.Helpers;
using FinTrack.Application.Common.Interfaces;
using FinTrack.Application.Requests.Base.BonusReceivable.Interfaces;
using FinTrack.Application.Requests.Base.BonusReceivable.ViewModels;
using FinTrack.Application.Requests.Base.Cash_Dividend.Interfaces;
using FinTrack.Application.Requests.Base.Cash_Dividend.ViewModels;
using FinTrack.Application.Requests.Base.Client_Base.Interfaces;
using FinTrack.Application.Requests.Base.Client_Base.ViewModels;
using FinTrack.Application.Requests.Base.Ipo.Interfaces;
using FinTrack.Application.Requests.Base.Ipo.ViewModels;
using FinTrack.Application.Requests.Base.PortfolioAccountBalance.Interfaces;
using FinTrack.Application.Requests.Base.Share.Interfaces;
using FinTrack.Application.Requests.Base.Share.ViewModels;
using FinTrack.Application.Requests.Consolidate.InterestCalculation.Interfaces;
using FinTrack.Application.Requests.Consolidate.Portfolio.Interfaces;
using FinTrack.Application.Requests.Consolidate.Portfolio.ViewModels;
using FinTrack.Application.Requests.Consolidate.Purchase_Power.Interfaces;
using FinTrack.Application.Requests.Jamuna.Client_Jamuna.Interfaces;
using FinTrack.Application.Requests.Jamuna.Client_Jamuna.ViewModels;
using FinTrack.Domain.Enums;
using FinTrack.Infrastructure.Data;
using FinTrack.Infrastructure.Migrations;
using FinTrack.Infrastructure.Services.Jamuna;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FinTrack.Infrastructure.Services.ConsolidatedService
{
    public class CosolidatedPortfolioReportService : SqlDbContextBase<CosolidatedPortfolioVM>, ICosolidatedPortfolioReportService
    {
        private readonly IClientBaseService _clientBaseService;
        private readonly IClientServiceJamuna _clientServiceJamuna;
        private readonly IShareBalanceService _sharebalanceService;
        private readonly IBonusReceivableService _bonusReceivableService;
        private readonly ICashDividendService _cashDividendService;
        private readonly IIpoApplicationService _ipoApplicationService;
        private readonly IPortfolioAccountBalanceService _portfolioAccountBalanceService;
        private readonly IConsolidatedPurchasePowerService _purchasePowerService;
        private readonly IMarginHelperService _marginHelperService;
        private readonly IUser _user;
        private readonly IInterestCalculationService _interestCalculationService;
        private readonly IServerDateTimeService _serverDateTimeService;
        public CosolidatedPortfolioReportService(IConfiguration configuration, IClientBaseService clientBaseService, IClientServiceJamuna clientServiceJamuna, IShareBalanceService sharebalanceService, IBonusReceivableService bonusReceivableService, ICashDividendService cashDividendService, IIpoApplicationService ipoApplicationService, IPortfolioAccountBalanceService portfolioAccountBalanceService, IConsolidatedPurchasePowerService purchasePowerService, IMarginHelperService marginHelperService, IUser user, IInterestCalculationService interestCalculationService, IServerDateTimeService serverDateTimeService) : base(configuration)
        {
            _clientBaseService = clientBaseService;
            _clientServiceJamuna = clientServiceJamuna;
            _sharebalanceService = sharebalanceService;
            _bonusReceivableService = bonusReceivableService;
            _cashDividendService = cashDividendService;
            _ipoApplicationService = ipoApplicationService;
            _portfolioAccountBalanceService = portfolioAccountBalanceService;
            _purchasePowerService = purchasePowerService;
            _marginHelperService = marginHelperService;
            _user = user;
            _interestCalculationService = interestCalculationService;
            _serverDateTimeService = serverDateTimeService;
        }

        [Obsolete]
        public async Task<CosolidatedPortfolioVM> GetCosolidatedPortfolioReport(string memberId, DateTime endDate)       
        {
            var currentTime =await _serverDateTimeService.GetServerDateTimeAsync();
            CosolidatedPortfolioVM reportPortfolioVm = new CosolidatedPortfolioVM();

            DateTime startDate = new DateTime();
            reportPortfolioVm.Message = string.Empty;
            reportPortfolioVm.ShareBalances = new List<ShareBalanceVM>();
            reportPortfolioVm.BonusReceivables = new List<BonusReceivableVM>();
            reportPortfolioVm.CashDividends = new List<CashDividendVM>();
            reportPortfolioVm.IpoApplications = new List<IpoApplicationVM>();
            reportPortfolioVm.ClientBalance = new ClientBalanceVM();
            reportPortfolioVm.ClientDetails = new ClientDetailsVM();
            reportPortfolioVm.CapitalGainLoss = new CapitalGainLossVM();
            reportPortfolioVm.PurchasePowerEquity = new PurchasePowerEquityVM();
            reportPortfolioVm.FundStatus = new FundStatusVM();
            reportPortfolioVm.MarginStatus = new MarginStatusVM();
            reportPortfolioVm.AccountHealthMessage = new CustomMessageVM();
            reportPortfolioVm.AccountHealthMessagePDF = new CustomMessageVM();

            Client_InfosVM clientInfosBase = new Client_InfosVM();
            ClientInfosJamunaVM clientInfosJamuna = new ClientInfosJamunaVM();

            if (_user.Role == "Guest")
            {
                reportPortfolioVm.Message = "You do not have permission to view this report!";
                return reportPortfolioVm;
            }

            if (memberId.Contains("g") || memberId.Contains("G"))
            {
                memberId = memberId.Contains("g") ? memberId.TrimStart('g') : memberId.Contains("G") ? memberId.TrimStart('G'): memberId;
                memberId = memberId.PadLeft(7,'0');
                clientInfosBase =await _clientBaseService.GetClientInfos(memberId);
                if (clientInfosBase != null)
                {
                    clientInfosJamuna = await _clientServiceJamuna.GetClientInfosJamuna(memberId);
                    if (clientInfosJamuna == null)
                    {
                        reportPortfolioVm.Message = "Member information is not found!";
                        return reportPortfolioVm;
                    }
                }
                else
                {
                    reportPortfolioVm.Message = "Member information is not found!";
                    return reportPortfolioVm;
                }
            }
            else
            {
                memberId = memberId.PadLeft(6, '0').PadLeft(8, '7');
                clientInfosJamuna = await _clientServiceJamuna.GetClientInfosJamuna(memberId);
                if (clientInfosJamuna != null)
                {
                    if (!string.IsNullOrEmpty(clientInfosJamuna.ClientCode) )
                    {
                        clientInfosBase = await _clientBaseService.GetClientInfos(clientInfosJamuna.ClientCode.PadLeft(7,'0'));
                        if (clientInfosBase == null)
                        {
                            reportPortfolioVm.Message = "Client information is not found!";
                            return reportPortfolioVm;
                        }
                    }
                    else
                    {
                        reportPortfolioVm.Message = $"Member Id {memberId} is not a client of gsl!";
                        return reportPortfolioVm;
                    }
                }
                else 
                {
                    reportPortfolioVm.Message = "Member information is not found!";
                    return reportPortfolioVm;
                }
            }

            if (clientInfosBase.Bo_Opening_Date.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) == "01/01/0001")
            {
                startDate = clientInfosBase.Acc_Opening_Date.Date;
            }
            else
                startDate = clientInfosBase.Bo_Opening_Date.Date.AddDays(-7);

            reportPortfolioVm.ShareBalances =await _sharebalanceService.GetShareBalances(clientInfosBase.Client_Code, endDate);
            reportPortfolioVm.BonusReceivables = await _bonusReceivableService.GetBonusReceivableInfo(clientInfosBase.Client_Code, endDate);
            reportPortfolioVm.CashDividends = await _cashDividendService.GetCashDividendInfo(clientInfosBase.Client_Code, endDate);
            reportPortfolioVm.IpoApplications = await _ipoApplicationService.GetIpoApplicationInfo(clientInfosBase.BP_ID);
            reportPortfolioVm.PortfolioAccountBalance = await _portfolioAccountBalanceService.GetPortfolioAccountBalance(clientInfosBase.Client_Code,startDate,endDate);
            reportPortfolioVm.Client_Type = clientInfosBase.Client_Type;

            var purchasePowerDetails = await _purchasePowerService.GetConsolidatedPurchasePower(clientInfosBase.Client_Code,endDate);
            var accrued_Interest =await _interestCalculationService.GetInterest(clientInfosBase.Client_Code, endDate, clientInfosBase.Client_Type, clientInfosBase.BP_ID);
            
            if (purchasePowerDetails != null)
            {
                //Balance Section
                reportPortfolioVm.ClientBalance.LB_Globe = purchasePowerDetails.LB_Globe;
                reportPortfolioVm.ClientBalance.MB_Globe = purchasePowerDetails.MB_Globe;
                reportPortfolioVm.ClientBalance.LB_Jamuna = purchasePowerDetails.LedgerBalanceJamuna;
                reportPortfolioVm.ClientBalance.MB_Jamuna = purchasePowerDetails.MatureBalanceJamuna;
                reportPortfolioVm.ClientBalance.LB_Consolidate = purchasePowerDetails.LedgerBalanceConsolidate;
                reportPortfolioVm.ClientBalance.MB_Consolidate = purchasePowerDetails.MatureBalanceConsolidate;

                //Client Details Section
                reportPortfolioVm.ClientDetails.Name =clientInfosBase.BP_Name;
                reportPortfolioVm.ClientDetails.Code =clientInfosBase.Client_Code.TrimStart('0');
                reportPortfolioVm.ClientDetails.JamunaMemberID = clientInfosJamuna.JamunaMemberCode.TrimStart('7').TrimStart('0');
                reportPortfolioVm.ClientDetails.AsOn =endDate.ToString("dd-MMMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                reportPortfolioVm.ClientDetails.Type_Name_Marketing = purchasePowerDetails.Type_Name_Marketing_Jamuna; ;
                reportPortfolioVm.ClientDetails.AccountStatus = GetAccountStatus(clientInfosBase.Is_Continued, clientInfosBase.Is_Dormant, clientInfosBase.IS_Suspended);
                DateTime lastTxnDate = await GetLastTransactionAsync(clientInfosBase.BP_ID);
                reportPortfolioVm.ClientDetails.LastTranDate = lastTxnDate.ToString("dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);

                //Gain Loss Section
                reportPortfolioVm.CapitalGainLoss.RealisedGain = reportPortfolioVm.PortfolioAccountBalance.RealisedGL;
                reportPortfolioVm.CapitalGainLoss.UnrealisedGain = reportPortfolioVm.ShareBalances.Sum(x => (x.Total_Quantity.Value * x.Market_price) - (x.Total_Quantity.Value * x.Avg_Rate));
                reportPortfolioVm.CapitalGainLoss.TotalGainOrLoss = reportPortfolioVm.PortfolioAccountBalance.RealisedGL + reportPortfolioVm.ShareBalances.Sum(x => (x.Total_Quantity * x.Market_price) - (x.Total_Quantity * x.Avg_Rate));

                //Purchase Power & Equity Section
                reportPortfolioVm.PurchasePowerEquity.PurchasePowerConsolidated = purchasePowerDetails.PurchasePower;
                reportPortfolioVm.PurchasePowerEquity.EquityConsolidated = purchasePowerDetails.EquityConsolidated;

                //Fund Status Section
                reportPortfolioVm.FundStatus.PendingWithdrawal = purchasePowerDetails.PendingWithdrawal;
                reportPortfolioVm.FundStatus.PendingDeposite = purchasePowerDetails.PendingDeposite;
                reportPortfolioVm.FundStatus.AccruedInterest = accrued_Interest;
                reportPortfolioVm.FundStatus.FundAvailableToWithdrawal = purchasePowerDetails.FundAvailableToWithdrawal - accrued_Interest;

                //Margin Status Section 
                reportPortfolioVm.MarginStatus.LoanTypeDescription = purchasePowerDetails.LoanTypeDescriptionJamuna;
                reportPortfolioVm.MarginStatus.MML = purchasePowerDetails.MaxMarginLimitJSCCL;
                reportPortfolioVm.MarginStatus.EML = purchasePowerDetails.EffectiveMarginLimitJSCCL;
                reportPortfolioVm.MarginStatus.PenWarLTV = purchasePowerDetails.PenWarLTV_Jamuna;
                reportPortfolioVm.MarginStatus.MarCalLTV = purchasePowerDetails.MarCalLTV_Jamuna;
                reportPortfolioVm.MarginStatus.LiqLTV = purchasePowerDetails.LiqLTV_Jamuna;

                var Loan = Math.Abs(Math.Min(0, purchasePowerDetails.LedgerBalanceJamuna + purchasePowerDetails.LB_Globe));
                var marginRiskData = _marginHelperService.GetMarginRiskData(purchasePowerDetails.Is_long_term_Jamuna, purchasePowerDetails.AlocatedMarginLimitJSCCL, Loan, purchasePowerDetails.EffectiveMarginLimitJSCCL, purchasePowerDetails.ShareMarketValue, purchasePowerDetails.PenWarLTV_Jamuna, purchasePowerDetails.MarCalLTV_Jamuna, purchasePowerDetails.MarCalTargetLTV_Jamuna, purchasePowerDetails.LiqLTV_Jamuna, purchasePowerDetails.Penal_Fee_Start_LTV, purchasePowerDetails.Authorized_LTV, purchasePowerDetails.LiqTargetLTV_Jamuna);
                reportPortfolioVm.MarginStatus.Current_LTV = marginRiskData.Current_LTV;
               
                if (marginRiskData.Authorized_LTV != 0)
                { reportPortfolioVm.MarginStatus.Authorized_LTV = marginRiskData.Authorized_LTV * 100; }
                else { reportPortfolioVm.MarginStatus.Authorized_LTV = 0; }

                if (marginRiskData.Current_LTV != 0)
                { reportPortfolioVm.MarginStatus.Current_LTV = marginRiskData.Current_LTV * 100; }
                else { reportPortfolioVm.MarginStatus.Current_LTV = 0; }

                if (purchasePowerDetails.PenWarLTV_Jamuna != 0 && Loan != 0)
                { reportPortfolioVm.MarginStatus.LoanOverUsageStockValue = Loan / purchasePowerDetails.PenWarLTV_Jamuna; }
                else { reportPortfolioVm.MarginStatus.LoanOverUsageStockValue = 0; }

                if (purchasePowerDetails.MarCalLTV_Jamuna != 0 && Loan != 0)
                { reportPortfolioVm.MarginStatus.MarginCallStockValue = Loan / purchasePowerDetails.MarCalLTV_Jamuna; }
                else { reportPortfolioVm.MarginStatus.MarginCallStockValue = 0; }

                if (purchasePowerDetails.LiqLTV_Jamuna != 0 && Loan != 0)
                { reportPortfolioVm.MarginStatus.LiquidationStockValue = Loan / purchasePowerDetails.LiqLTV_Jamuna; }
                else { reportPortfolioVm.MarginStatus.LiquidationStockValue = 0; }

                reportPortfolioVm.MarginStatus.LoanOverUsage = Math.Max(0, Loan - purchasePowerDetails.EffectiveMarginLimitJSCCL);

                //Account Health Section
                var marginActionMessage = _marginHelperService.GetMarginActionMessage(marginRiskData.Risk_Status, marginRiskData.Depo_Buy_Req, marginRiskData.Adjust_Req, marginRiskData.Sell_Req, purchasePowerDetails.Penal_Fee_Start_LTV, marginRiskData.Authorized_LTV, purchasePowerDetails.MarCalLTV_Jamuna, purchasePowerDetails.MarCalTargetLTV_Jamuna, purchasePowerDetails.LiqLTV_Jamuna, purchasePowerDetails.LiqTargetLTV_Jamuna, purchasePowerDetails.ShareMarketValue, Loan);                
                reportPortfolioVm.AccountHealthMessage.CustomMessageBody = marginActionMessage.CustomMessageBody;
                reportPortfolioVm.AccountHealthMessage.CustomMessageHeader = marginActionMessage.CustomMessageHeader;
                reportPortfolioVm.AccountHealthMessage.ColorCode = marginActionMessage.ColorCode;
                reportPortfolioVm.AccountHealthMessage.FontColorCode = marginActionMessage.FontColorCode;

                var marginActionMessagePDF = _marginHelperService.GetMarginActionMessageforPDF(marginRiskData.Risk_Status, marginRiskData.Depo_Buy_Req, marginRiskData.Adjust_Req, marginRiskData.Sell_Req, purchasePowerDetails.Penal_Fee_Start_LTV, marginRiskData.Authorized_LTV, purchasePowerDetails.MarCalLTV_Jamuna, purchasePowerDetails.MarCalTargetLTV_Jamuna, purchasePowerDetails.LiqLTV_Jamuna, purchasePowerDetails.LiqTargetLTV_Jamuna, purchasePowerDetails.ShareMarketValue, Loan);
                reportPortfolioVm.AccountHealthMessagePDF.CustomMessageBody = marginActionMessagePDF.CustomMessageBody;
                reportPortfolioVm.AccountHealthMessagePDF.CustomMessageHeader = marginActionMessagePDF.CustomMessageHeader;
                reportPortfolioVm.AccountHealthMessagePDF.ColorCode = marginActionMessagePDF.ColorCode;
                reportPortfolioVm.AccountHealthMessagePDF.FontColorCode = marginActionMessagePDF.FontColorCode;

                var Params = string.Format("{0}/{1}/{2}/{3}", clientInfosJamuna.JamunaMemberCode.TrimStart('7').TrimStart('0'), endDate.ToString("dd-MMMM-yyyy", System.Globalization.CultureInfo.InvariantCulture), (int)ReportType.CosolidatedPortfolio, currentTime.ToString("dd-MMMM-yyyy h:mm tt", System.Globalization.CultureInfo.InvariantCulture));
                string encryptedParams = CryptoHelpers.EncryptStringAES(Params, AppConstant.CryptoSecret);
                reportPortfolioVm.Params = encryptedParams;
            }   
            return reportPortfolioVm;
        }
        public async Task<DateTime> GetLastTransactionAsync(int bpID)
        {
            string query = $"select  Max(Transaction_Date) as 'TransactionDate' from Client_Transaction where Client_BP_ID = {bpID}";
            var date = await GetDateTimeAsync(query, null);
            return date;
        }
        public string GetAccountStatus(bool isContinue, bool isDormant, bool isSuspended)
        {
            string accountStatus = string.Empty;
            if (isContinue == false)
            {
                accountStatus = "Closed";
            }
            else if (isContinue == true && isDormant == false)
            {
                accountStatus = "Active";
            }
            else
            {
                accountStatus = "Dormant";
            }

            if (isSuspended)
            {
                accountStatus = accountStatus + "[Suspended]";
            }
            return accountStatus;
        }
    }
}
