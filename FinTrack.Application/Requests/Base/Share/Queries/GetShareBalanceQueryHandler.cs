using FinTrack.Application.Requests.Base.Share.Interfaces;
using FinTrack.Application.Requests.Base.Share.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.Share.Queries
{
    public class GetShareBalanceQueryHandler : IRequestHandler<GetShareBalanceQuery, List<ShareBalanceVM>>
    {
        private readonly IShareBalanceService _shareBalanceService;
        public GetShareBalanceQueryHandler(IShareBalanceService shareBalanceService)
        {
            _shareBalanceService = shareBalanceService ?? throw new ArgumentNullException(nameof(_shareBalanceService));
        }
        public Task<List<ShareBalanceVM>> Handle(GetShareBalanceQuery request, CancellationToken cancellationToken)
        {
            var data = _shareBalanceService.GetShareBalances(request.ClientCode, request.EndDate);
            return data;
        }
    }
}
