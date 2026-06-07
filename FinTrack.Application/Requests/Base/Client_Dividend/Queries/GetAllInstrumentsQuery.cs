using FinTrack.Application.Requests.Base.Client_Dividend.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.Client_Dividend.Queries
{
    public class GetAllInstrumentsQuery: IRequest<List<InstrumentVM>>
    {
        public GetAllInstrumentsQuery() { }
    }
}
