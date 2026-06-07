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
    public class ForgetPasswordCommandHandler : IRequestHandler<ForgetPasswordCommand, ResultModel>
    {
        private readonly IIdentityService _identityService;
        public ForgetPasswordCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(_identityService));
        }
        public Task<ResultModel> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = _identityService.ForgetPassword(request.Email);
            return result;
        }
    }
}
