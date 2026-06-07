using AutoMapper;
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
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
    {
        private readonly IIdentityService _identityService;
        public ChangePasswordCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(_identityService));           
        }
        public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            return await _identityService.ChangePassword(request.OldPassword,request.NewPassword);
        }
    }
}
