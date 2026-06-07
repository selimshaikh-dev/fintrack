using FinTrack.Application.Requests.Base.Cash_Dividend.Interfaces;
using FinTrack.Application.Requests.Base.Client_Dividend.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.Client_Dividend.Queries
{
    public class CheckInterimDividendQueryHandler : IRequestHandler<CheckInterimDividendQuery, bool>
    {
        private readonly IInstrumentService _instrumentService;

        public CheckInterimDividendQueryHandler(IInstrumentService instrumentRepo)
        {
            _instrumentService = instrumentRepo ?? throw new ArgumentNullException(nameof(_instrumentService));
        }

        public async Task<bool> Handle(CheckInterimDividendQuery request, CancellationToken cancellationToken)
        {
            var data = await _instrumentService.CheckInterimDividend(request.InstrumentId);
            return data;
        }
    }
}