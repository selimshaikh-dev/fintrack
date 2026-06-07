using FinTrack.Application.Common.Interfaces;
using FinTrack.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Auth.Commands
{
    public class SetPasswordCommandHandler : IRequestHandler<SetPasswordCommand, ResultModel>
    {
        private readonly IIdentityService _identityService;
        public SetPasswordCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }
        public async Task<ResultModel> Handle(SetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _identityService.SetPassword(request.Id,request.Password);
            return result;
        }
    }
}
