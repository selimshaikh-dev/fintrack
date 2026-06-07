using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.TaxReport.ViewModels
{
    public class TaxCertificateReportJamunaVM
    {
        public string Message { get; set; }
        public string PrintDate { get; set; }
        public string Params { get; set; }
        public ClientDetailsTaxVM ClientDetails { get; set; }
        public CashFlowJamunaVM CashFlow { get; set; }
    }
    public class CashFlowJamunaVM
    {
        public decimal TotalDeposite { get; set; }
        public decimal TotalWithdraw { get; set; }
        public decimal TotalCharges { get; set; }
        public decimal NetDeposite { get; set; }
        public decimal FDB { get; set; }
        public decimal LedgerBalance { get; set; }
        public decimal OpeningAsset { get; set; }
        public decimal ClosingAsset { get; set; }
        public decimal ProfitReceive { get; set; }
        public decimal AIT { get; set; }
        public string AsOn { get; set; }
        public decimal ShareCapitalBalance { get; set; }
        public decimal SubscriptionBalance { get; set; }
        public decimal DividendBalance { get; set; }
    }
    public class ClientDetailsTaxVM
    {
        public string MemberId { get; set; }
        public string TIN { get; set; }
        public string Name { get; set; }
        public string Father_Name { get; set; }
        public string Mother_Name { get; set; }
        public string Address { get; set; }
        public string SubjectLine { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string NID { get; set; }
    }
}
