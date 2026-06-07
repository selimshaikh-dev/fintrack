using FinTrack.Application.AuthRole.Interfaces;
using FinTrack.Application.Requests.Jamuna.Client_Jamuna.Interfaces;
using FinTrack.Application.Requests.Jamuna.Client_Jamuna.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.Client_Jamuna.Queries
{
    public class GetClientInfosByEmailQueryHandler : IRequestHandler<GetClientInfosByEmailQuery, ClientInfosJamunaVM>
    {
        private readonly IClientServiceJamuna _clientService;
        public GetClientInfosByEmailQueryHandler(IClientServiceJamuna clientService)
        {
            _clientService = clientService ?? throw new ArgumentNullException(nameof(_clientService));
        }
        public async Task<ClientInfosJamunaVM> Handle(GetClientInfosByEmailQuery request, CancellationToken cancellationToken)
        {
            var clientDetails = await _clientService.GetClientInfosByEmail(request.Email);
            return clientDetails;
        }
    }
}
