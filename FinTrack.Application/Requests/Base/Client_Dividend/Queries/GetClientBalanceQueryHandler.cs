using FinTrack.Application.Requests.Base.Client_Dividend.Interfaces;
using FinTrack.Application.Requests.Base.Client_Dividend.ViewModels;
using FinTrack.Application.Requests.Jamuna.Client_Ledger_Details.Interfaces;
using FinTrack.Application.Requests.Jamuna.Client_Ledger_Details.Queries;
using FinTrack.Application.Requests.Jamuna.Client_Ledger_Details.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.Client_Dividend.Queries
{
    public class GetClientBalanceQueryHandler : IRequestHandler<GetClientBalanceQuery, ClientBalanceInfoVM>
    {
        private readonly IClientBalanceInfoService _clientCashDividendService;

        public GetClientBalanceQueryHandler(IClientBalanceInfoService clientCashDividendService)
        {
            _clientCashDividendService = clientCashDividendService ?? throw new ArgumentNullException(nameof(_clientCashDividendService));
        }
        public async Task<ClientBalanceInfoVM> Handle(GetClientBalanceQuery request, CancellationToken cancellationToken)
        {
            var data = await _clientCashDividendService.GetClientBalanceInfo(request.ClientCode);
            return data;
        }
    }
}