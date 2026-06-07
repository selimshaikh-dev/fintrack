using FinTrack.Application.Requests.Base.Client_Base.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.Client_Base.Queries
{
    public class GetClientInfosBaseQuery : IRequest<Client_InfosVM>
    {
        public string ClientCode { get; set; }
        public GetClientInfosBaseQuery(string clientCode)
        {
            ClientCode = clientCode;
        }
    }
}
