using FinTrack.Application.Requests.Jamuna.Client_Ledger_Details.Interfaces;
using FinTrack.Application.Requests.Jamuna.Client_Ledger_Details.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.Client_Ledger_Details.Queries
{
    public class GetClientLedgerDetailsQueryHandler : IRequestHandler<GetClientLedgerDetailsQuery, DailyLedgerVM>
    {
        private readonly IClientLedgerDetailsService _clientLedgerDetailsService;
        public GetClientLedgerDetailsQueryHandler(IClientLedgerDetailsService clientLedgerDetailsService)
        {
            _clientLedgerDetailsService = clientLedgerDetailsService ?? throw new ArgumentNullException(nameof(_clientLedgerDetailsService));
        }
        public async Task<DailyLedgerVM> Handle(GetClientLedgerDetailsQuery request, CancellationToken cancellationToken)
        {
            var data = await _clientLedgerDetailsService.GetClientLedgerDetails(request.MemberID, request.StartDate, request.EndDate);
            return data;
        }
    }
}
