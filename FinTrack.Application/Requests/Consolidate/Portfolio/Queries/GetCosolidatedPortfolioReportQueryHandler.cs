using FinTrack.Application.Requests.Consolidate.Portfolio.Interfaces;
using FinTrack.Application.Requests.Consolidate.Portfolio.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Consolidate.Portfolio.Queries
{
    public class GetCosolidatedPortfolioReportQueryHandler : IRequestHandler<GetCosolidatedPortfolioReportQuery, CosolidatedPortfolioVM>
    {
        private readonly ICosolidatedPortfolioReportService _portfolioReportService;
        public GetCosolidatedPortfolioReportQueryHandler(ICosolidatedPortfolioReportService portfolioReportService)
        {
            _portfolioReportService = portfolioReportService ?? throw new ArgumentNullException(nameof(_portfolioReportService));
        }
        public async Task<CosolidatedPortfolioVM> Handle(GetCosolidatedPortfolioReportQuery request, CancellationToken cancellationToken)
        {
            var data = await _portfolioReportService.GetCosolidatedPortfolioReport(request.MemberID, request.EndDate);
            return data;
        }
    }
}
