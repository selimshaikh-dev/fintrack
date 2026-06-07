using AutoMapper;
using FinTrack.Application.Auth.ViewModels;
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
    public class RegisterCommandHandler: IRequestHandler<RegisterCommand, ResultModel>
    {
        private readonly IIdentityService _identityService;
        private readonly IMapper _mapper;
        public RegisterCommandHandler(IIdentityService identityService, IMapper mapper)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }
        public async Task<ResultModel> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<RegisterVM>(request);
            var result = await _identityService.RegisterUserAsync(user);
            return result;
        }
    }
}
