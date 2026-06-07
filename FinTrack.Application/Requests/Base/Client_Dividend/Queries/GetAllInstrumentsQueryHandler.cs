using FinTrack.Application.Requests.Base.Client_Dividend.Interfaces;
using FinTrack.Application.Requests.Base.Client_Dividend.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.Client_Dividend.Queries
{
    public class GetAllInstrumentsQueryHandler : IRequestHandler<GetAllInstrumentsQuery, List<InstrumentVM>>
    {
        private readonly IInstrumentService _instrumentService;

        public GetAllInstrumentsQueryHandler(IInstrumentService instrumentService)
        {
            _instrumentService = instrumentService ?? throw new ArgumentNullException(nameof(_instrumentService));
        }

        public async Task<List<InstrumentVM>> Handle(GetAllInstrumentsQuery request, CancellationToken cancellationToken)
        {
            var data = await _instrumentService.GetAllInstrument();
            return data;
        }
    }
}