using FinTrack.Application.Requests.Jamuna.TaxReport.Interfaces;
using FinTrack.Application.Requests.Jamuna.TaxReport.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.TaxReport.Queries
{
    public class TaxCertificateQueryHandler : IRequestHandler<TaxCertificateQuery, TaxCertificateReportJamunaVM>
    {
        private readonly ITaxCertificateReportService _taxCertificateReportService;
        public TaxCertificateQueryHandler(ITaxCertificateReportService taxCertificateReportService)
        {
            _taxCertificateReportService = taxCertificateReportService ?? throw new ArgumentNullException(nameof(_taxCertificateReportService));
        }
        public async Task<TaxCertificateReportJamunaVM> Handle(TaxCertificateQuery request, CancellationToken cancellationToken)
        {
            var data = await _taxCertificateReportService.GetTaxCertificateReport(request.MemberID, request.FinancialYear);
            return data;
        }
    }
}
