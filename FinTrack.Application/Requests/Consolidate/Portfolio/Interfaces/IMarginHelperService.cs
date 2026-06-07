using FinTrack.Application.Requests.Consolidate.Portfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Consolidate.Portfolio.Interfaces
{
    public interface IMarginHelperService
    {
        public MarginRiskStatusVM GetMarginRiskData(bool is_long_term, decimal AMR, decimal Loan, decimal EML, decimal ShareMarketValue, decimal penWarLTV, decimal marCalLTV, decimal marCalTargetLTV, decimal liqLTV, decimal Penal_Fee_Start_LTV, decimal Authorized_LTV, decimal LiqTargetLTV_Jamuna);
        public CustomMessageVM GetMarginActionMessage(string Risk_Status, decimal Depo_Buy_Req, decimal Adjust_Req, decimal Sell_Req, decimal Penal_Fee_Start_LTV, decimal Authorized_LTV, decimal marCalLTV, decimal marCalTargetLTV, decimal liqLTV, decimal liqTargetLTV, decimal ShareMarketValue, decimal Loan);
        public CustomMessageVM GetMarginActionMessageforPDF(string Risk_Status, decimal Depo_Buy_Req, decimal Adjust_Req, decimal Sell_Req, decimal Penal_Fee_Start_LTV, decimal Authorized_LTV, decimal marCalLTV, decimal marCalTargetLTV, decimal liqLTV, decimal liqTargetLTV, decimal ShareMarketValue, decimal Loan);
    }
}
