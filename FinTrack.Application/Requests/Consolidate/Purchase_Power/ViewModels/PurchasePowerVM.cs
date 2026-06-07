using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Consolidate.Purchase_Power.ViewModels
{
    public class PurchasePowerVM
    {
        public int BPID { get; set; }
        public decimal FreeLedgerBalanceJamuna { get; set; }
        public decimal FreeMatureBalanceJamuna { get; set; }
        public decimal FreeLedgerBalanceGlobe { get; set; }
        public decimal FreeMatureBalanceGlobe { get; set; }
        public decimal ShareMarketValue { get; set; }
        public decimal MarginableShareMarketValue { get; set; }
        public decimal FreeLedgerBalanceConsolidate { get; set; }
        public decimal FreeMatureBalanceConsolidate { get; set; }
        public decimal LedgerBalanceConsolidate { get; set; }
        public decimal MatureBalanceConsolidate { get; set; }
        public decimal MaxMarginLimitJSCCL { get; set; }
        public decimal MarginLimitJSCCL { get; set; }
        public decimal MaxMarginLimitGlobe { get; set; }
        public decimal MarginLimitGlobe { get; set; }
        public decimal EquityConsolidated { get; set; }
        public decimal MarginEquityConsolidated { get; set; }
        public decimal AlocatedMarginLimitJSCCL { get; set; }
        public decimal AlocatedMarginLimitGlobe { get; set; }
        public decimal EffectiveMarginLimitJSCCL { get; set; }
        public decimal EffectiveMarginLimitGlobe { get; set; }
        public decimal PurchasePower { get; set; }
        public decimal MaturedBalanceNextDate { get; set; }
        public decimal Marginable_Purchase_Power_Globe { get; set; }
        public decimal NonMarginable_Purchase_Power_Globe { get; set; }
        public string JamunaMemberID { get; set; }
        public string LoanTypeDescriptionJamuna { get; set; }
        public string LoanTypeDescriptionGlobe { get; set; }
        public decimal LedgerBalanceJamuna { get; set; }
        public decimal MatureBalanceJamuna { get; set; }
        public decimal LB_Globe { get; set; }
        public decimal MB_Globe { get; set; }
        public bool Is_long_term_Globe { get; set; }
        public decimal PenWarLTV_Globe { get; set; }
        public decimal MarCalLTV_Globe { get; set; }
        public decimal MarCalTargetLTV_Globe { get; set; }
        public decimal LiqLTV_Globe { get; set; }
        public decimal LiqTargetLTV_Globe { get; set; }
        public string Type_Name_Marketing_Globe { get; set; }
        public bool Is_long_term_Jamuna { get; set; }
        public decimal PenWarLTV_Jamuna { get; set; }
        public decimal MarCalLTV_Jamuna { get; set; }
        public decimal MarCalTargetLTV_Jamuna { get; set; }
        public decimal LiqLTV_Jamuna { get; set; }
        public decimal LiqTargetLTV_Jamuna { get; set; }
        public string Type_Name_Marketing_Jamuna { get; set; }
        public decimal Authorized_LTV { get; set; }
        public decimal Penal_Fee_Start_LTV { get; set; }
        public decimal Equity_Globe { get; set; }
        public decimal Equity_Margin_Globe { get; set; }
        public int Client_Type { get; set; }
        public int LoanTypeGlobe { get; set; }
        public decimal PendingDeposite { get; set; }
        public decimal PendingWithdrawal { get; set; }
        public decimal FundAvailableToWithdrawal { get; set; }
    }
}
