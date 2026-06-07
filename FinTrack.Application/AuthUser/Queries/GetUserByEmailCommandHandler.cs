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
    public class GetUserByEmailCommandHandler : IRequestHandler<GetUserByEmailCommand, UserReturnVM>
    {
        private readonly IUserService _userService;
        public GetUserByEmailCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<UserReturnVM> Handle(GetUserByEmailCommand request, CancellationToken cancellationToken)
        {
            var user = await _userService.GetUserByEmailAsync(request.Email);
            return user;
        }
    }
}
