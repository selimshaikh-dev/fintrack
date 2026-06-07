using AutoMapper;
using FinTrack.Application.Auth.ViewModels;
using FinTrack.Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Auth.Commands
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, object>
    {
        private readonly IIdentityService _identityService;
        private IMapper _mapper;
        public LoginCommandHandler(IIdentityService identityService, IMapper mapper)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(_identityService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper)); ;
        }

        public async Task<object> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<LoginVM>(request);
            return await _identityService.Login(user);
        }
    }
}
