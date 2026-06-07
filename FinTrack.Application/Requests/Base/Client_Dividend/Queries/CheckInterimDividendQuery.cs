using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.Client_Dividend.Queries
{
    public class CheckInterimDividendQuery : IRequest<bool>
    {
        public int InstrumentId { get; }

        public CheckInterimDividendQuery(int instrumentId)
        {
            InstrumentId = instrumentId;
        }
    }
}