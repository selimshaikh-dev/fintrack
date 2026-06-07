using FinTrack.Application.AuthUser.Interfaces;
using FinTrack.Application.AuthUser.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.AuthUser.Queries
{
    public class GetUserByIdCommandHandler : IRequestHandler<GetUserByIdCommand, UserReturnVM>
    {
        private readonly IUserService _userService;
        public GetUserByIdCommandHandler(IUserService userService)
        {
                _userService = userService;
        }
        public async Task<UserReturnVM> Handle(GetUserByIdCommand request, CancellationToken cancellationToken)
        {
            var user =await _userService.GetUserByIdAsync(request.Id);
            return user;
        }
    }
}
