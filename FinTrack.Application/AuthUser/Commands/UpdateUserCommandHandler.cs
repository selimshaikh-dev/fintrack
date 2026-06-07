using AutoMapper;
using FinTrack.Application.AuthUser.Interfaces;
using FinTrack.Application.AuthUser.ViewModels;
using FinTrack.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthUser.Commands
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ResultModel>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UpdateUserCommandHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(_userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }
        public async Task<ResultModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<UpdateUserVM>(request);
            var result = await _userService.UpdateUserAsync(user);
            return result;
        }
    }
}
