using FinTrack.Application.Requests.Base.Client_Dividend.Interfaces;
using FinTrack.Application.Requests.Base.Client_Dividend.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.Client_Dividend.Queries
{
    public class GetClientwiseCashDividendQueryHandler : IRequestHandler<GetClientwiseCashDividendQuery, List<CashDividendVM>>
    {
        private readonly IClientCashDividendService _cashDividendService;

        public GetClientwiseCashDividendQueryHandler(IClientCashDividendService cashDividendService)
        {
            _cashDividendService = cashDividendService ?? throw new ArgumentNullException(nameof(_cashDividendService));
        }

        public async Task<List<CashDividendVM>> Handle(GetClientwiseCashDividendQuery request, CancellationToken cancellationToken)
        {
            var data = await _cashDividendService.GetClientCashDividend(request.ClientCode, request.InstrumentId);
            return data;
        }
    }
}
