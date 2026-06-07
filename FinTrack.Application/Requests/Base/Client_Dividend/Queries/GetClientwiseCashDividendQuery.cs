using FinTrack.Application.Requests.Base.Client_Dividend.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.Client_Dividend.Queries
{
    public class GetClientwiseCashDividendQuery : IRequest<List<CashDividendVM>>
    {
        public string ClientCode { get; set; }
        public int InstrumentId { get; set; }

        public GetClientwiseCashDividendQuery (string clientCode, int instrumentId)
        {
            ClientCode = clientCode;
            InstrumentId = instrumentId;
        }
    }
}