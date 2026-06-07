using FinTrack.Application.Requests.Consolidate.LedgerSummary.Interfaces;
using FinTrack.Application.Requests.Jamuna.Client_Ledger_Details.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Consolidate.LedgerSummary.Queries
{
    public class GetConsolidateLedgerSummaryQueryHandler : IRequestHandler<GetConsolidateLedgerSummaryQuery, DailyLedgerVM>
    {
        private readonly IConsolidatedLedgerSummaryService _consolidatedLedgerSummary;
        public GetConsolidateLedgerSummaryQueryHandler(IConsolidatedLedgerSummaryService consolidatedLedgerSummary)
        {
            _consolidatedLedgerSummary = consolidatedLedgerSummary ?? throw new ArgumentNullException(nameof(_consolidatedLedgerSummary));
        }
        public async Task<DailyLedgerVM> Handle(GetConsolidateLedgerSummaryQuery request, CancellationToken cancellationToken)
        {
            var data = await _consolidatedLedgerSummary.GetConsolidatedLedgerSummaryAsync(request.MemberID, request.StartDate, request.EndDate);
            return data;
        }
    }
}
