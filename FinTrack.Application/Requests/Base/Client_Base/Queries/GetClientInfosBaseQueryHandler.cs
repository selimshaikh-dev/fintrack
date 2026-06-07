using FinTrack.Application.Requests.Base.Client_Base.Interfaces;
using FinTrack.Application.Requests.Base.Client_Base.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.Client_Base.Queries
{
    public class GetClientInfosBaseQueryHandler : IRequestHandler<GetClientInfosBaseQuery, Client_InfosVM>
    {
        private readonly IClientBaseService _clientBaseService;
        public GetClientInfosBaseQueryHandler(IClientBaseService clientBaseService)
        {
            _clientBaseService = clientBaseService ?? throw new ArgumentNullException(nameof(clientBaseService));
        }
        public async Task<Client_InfosVM> Handle(GetClientInfosBaseQuery request, CancellationToken cancellationToken)
        {
            var data = await _clientBaseService.GetClientInfos(request.ClientCode);
            return data;
        }
    }
}
