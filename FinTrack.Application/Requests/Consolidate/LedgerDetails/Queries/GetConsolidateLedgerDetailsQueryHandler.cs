using FinTrack.Application.Requests.Consolidate.LedgerDetails.Interfaces;
using FinTrack.Application.Requests.Jamuna.Client_Ledger_Details.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Consolidate.LedgerDetails.Queries
{
    public class GetConsolidateLedgerDetailsQueryHandler : IRequestHandler<GetConsolidateLedgerDetailsQuery, DailyLedgerVM>
    {
        private readonly IConsolidatedLedgerDetailsService _ledgerDetailsService;
        public GetConsolidateLedgerDetailsQueryHandler(IConsolidatedLedgerDetailsService ledgerDetailsService)
        {
            _ledgerDetailsService = ledgerDetailsService ?? throw new ArgumentNullException(nameof(_ledgerDetailsService));
        }
        public async Task<DailyLedgerVM> Handle(GetConsolidateLedgerDetailsQuery request, CancellationToken cancellationToken)
        {
            var data = await _ledgerDetailsService.GetConsolidatedLedgerBalanceAsync(request.MemberID,request.StartDate,request.EndDate);
            return data;
        }
    }
}
