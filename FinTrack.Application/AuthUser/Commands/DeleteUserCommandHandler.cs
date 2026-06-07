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
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ResultModel>
    {
        private readonly IUserService _userService; 
        public DeleteUserCommandHandler(IUserService userService)
        {
                _userService = userService;
        }
        public Task<ResultModel> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var result = _userService.DeleteUserAsync(request.Id);
            return result;
        }
    }
}
