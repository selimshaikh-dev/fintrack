using FinTrack.Application.Requests.Jamuna.TaxReport.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.TaxReport.Interfaces
{
    public interface ITaxCertificateReportService :IDisposable
    {
        Task<TaxCertificateReportJamunaVM> GetTaxCertificateReport(string memberID, string financialYear);
    }
}
