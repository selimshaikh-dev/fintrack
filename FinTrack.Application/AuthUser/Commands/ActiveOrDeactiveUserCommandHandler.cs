using FinTrack.Application.AuthUser.Interfaces;
using FinTrack.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthUser.Commands
{
    public class ActiveOrDeactiveUserCommandHandler : IRequestHandler<ActiveOrDeactiveUserCommand, ResultModel>
    {
        private readonly IUserService _userService;
        public ActiveOrDeactiveUserCommandHandler(IUserService userService)
        {
                _userService = userService;
        }
        public async Task<ResultModel> Handle(ActiveOrDeactiveUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _userService.ActiveOrDeactiveUserAsync(request.Id, request.IsActive);
            return result;
        }
    }
}
