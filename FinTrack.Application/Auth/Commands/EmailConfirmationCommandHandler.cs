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
    public class EmailConfirmationCommandHandler : IRequestHandler<EmailConfirmationCommand, ResultModel>
    {
        private readonly IIdentityService _identityService;

        public EmailConfirmationCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        }

        public async Task<ResultModel> Handle(EmailConfirmationCommand request, CancellationToken cancellationToken)
        {

            var result = await _identityService.VerifyEmail(request.Id, request.Email);

            return result;
        }
    }
}
