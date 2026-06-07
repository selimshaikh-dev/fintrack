using FinTrack.Application.AuthRole.Commands;
using FinTrack.Application.AuthRole.Interfaces;
using FinTrack.Application.Common.Models;
using FinTrack.Application.Requests.Jamuna.Menus_Url.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.Menus_Url.Commands
{
    public class DeleteMenuUrlCommandHandler : IRequestHandler<DeleteMenuUrlCommand, ResultModel>
    {
        private readonly IMenusUrlService _menusUrlService;
        public DeleteMenuUrlCommandHandler(IMenusUrlService menusUrlService)
        {
            _menusUrlService = menusUrlService ?? throw new ArgumentNullException(nameof(_menusUrlService));
        }
        public async Task<ResultModel> Handle(DeleteMenuUrlCommand request, CancellationToken cancellationToken)
        {
            var result = await _menusUrlService.DeleteMenusUrl(request.Id);
            return result;
        }
    }
}
