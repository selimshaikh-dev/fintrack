using AutoMapper;
using FinTrack.Application.AuthView.Commands;
using FinTrack.Application.AuthView.Interfaces;
using FinTrack.Application.AuthView.Models;
using FinTrack.Application.Commands.Base.Interfaces;
using FinTrack.Application.Common.Models;
using FinTrack.Application.Requests.Base.Client_Dividend.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Commands.Base.ClientCashDividend
{
    public class AddCashDividendCommandHandler : IRequestHandler<AddCashDividendCommand, Result>
    {
        private readonly IAddCashDividendService _addCashDividendService;
        private readonly IMapper _mapper;

        public AddCashDividendCommandHandler(IAddCashDividendService addCashDividendService, IMapper mapper)
        {
            _addCashDividendService = addCashDividendService ?? throw new ArgumentNullException(nameof(_addCashDividendService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(_mapper));
        }
        public async Task<Result> Handle(AddCashDividendCommand request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<CashDividendVM>(request);
            var result = await _addCashDividendService.AddCashDividend(data);
            return result;
        }
    }
}