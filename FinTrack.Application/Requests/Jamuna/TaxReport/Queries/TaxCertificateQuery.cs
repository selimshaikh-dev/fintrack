using FinTrack.Application.Requests.Jamuna.TaxReport.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.TaxReport.Queries
{
    public class TaxCertificateQuery : IRequest<TaxCertificateReportJamunaVM>
    {
        public string MemberID { get; set; }
        public string FinancialYear { get; set; }
        public TaxCertificateQuery(string memberID, string financialYear)
        {
            MemberID = memberID;
            FinancialYear = financialYear;
        }
    }
}
