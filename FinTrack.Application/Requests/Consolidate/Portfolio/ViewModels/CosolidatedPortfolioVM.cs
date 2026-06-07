using FinTrack.Application.Requests.Base.BonusReceivable.ViewModels;
using FinTrack.Application.Requests.Base.Cash_Dividend.ViewModels;
using FinTrack.Application.Requests.Base.Ipo.ViewModels;
using FinTrack.Application.Requests.Base.PortfolioAccountBalance.ViewModels;
using FinTrack.Application.Requests.Base.Share.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Consolidate.Portfolio.ViewModels
{
    public class CosolidatedPortfolioVM
    {
        public string Message { get; set; }
        public int Client_Type { get; set; }
        public string Params { get; set; }
        public List<ShareBalanceVM>  ShareBalances { get; set; }
        public PortfolioAccountBalanceVM PortfolioAccountBalance { get; set; }
        public IEnumerable<BonusReceivableVM> BonusReceivables { get; set; }
        public IEnumerable<CashDividendVM> CashDividends { get; set; }
        public IEnumerable<IpoApplicationVM> IpoApplications { get; set; }      
        public ClientBalanceVM ClientBalance { get; set; }
        public CapitalGainLossVM CapitalGainLoss { get; set; }
        public PurchasePowerEquityVM PurchasePowerEquity { get; set; }
        public FundStatusVM FundStatus { get; set; }
        public CustomMessageVM AccountHealthMessage { get; set; }
        public CustomMessageVM AccountHealthMessagePDF { get; set; }
        public MarginStatusVM MarginStatus { get; set; }    
        public ClientDetailsVM ClientDetails { get; set; }

    }
}
