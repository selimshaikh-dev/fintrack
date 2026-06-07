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

namespace FinTrack.Application.AuthUser.Queries
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, IEnumerable<UserReturnVM>>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public GetUserQueryHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(_userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }
        public async Task<IEnumerable<UserReturnVM>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var searchItem = new UserQueryVM
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
            var data = await _userService.GetUsersAsync(searchItem);
            return data;
        }
    }
}
