using AutoMapper;
using FinTrack.Application.AuthRole.ViewModels;
using FinTrack.Application.Common.Models;
using FinTrack.Application.Requests.Jamuna.Menus_Url.Interfaces;
using FinTrack.Application.Requests.Jamuna.Menus_Url.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.Menus_Url.Commands
{
    public class CreateMenusUrlCommandHandler : IRequestHandler<CreateMenusUrlCommand, ResultModel>
    {
        private readonly IMenusUrlService _menusUrlService;
        private readonly IMapper _mapper;
        public CreateMenusUrlCommandHandler(IMenusUrlService menusUrlService, IMapper mapper)
        {
            _menusUrlService = menusUrlService ?? throw new ArgumentNullException(nameof(_menusUrlService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }
        public async Task<ResultModel> Handle(CreateMenusUrlCommand request, CancellationToken cancellationToken)
        {
            var menusUrl = new MenusUrlVM { Id = request.Id, Name = request.Name };
            var result = await _menusUrlService.CreateMenusUrl(menusUrl);
            return result;
        }
    }
}
