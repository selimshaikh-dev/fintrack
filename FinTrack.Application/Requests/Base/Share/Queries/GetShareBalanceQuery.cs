using FinTrack.Application.Requests.Base.Share.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.Share.Queries
{
    public class GetShareBalanceQuery: IRequest<List<ShareBalanceVM>>
    {
        public string ClientCode { get; set; }
        public DateTime EndDate { get; set; }
        public GetShareBalanceQuery(string clientCode, DateTime endDate) 
        {
            ClientCode=clientCode;
            EndDate=endDate;
        }
    }
}
