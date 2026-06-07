using FinTrack.Application.Requests.Jamuna.Financial_Year.Interfaces;
using FinTrack.Application.Requests.Jamuna.Financial_Year.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.Financial_Year.Queries
{
    public class GetFinancialYearQueryHandler : IRequestHandler<GetFinancialYearQuery, List<FinancialDropdownVM>>
    {
        private readonly IFinancialYearService _financialYearService;
        public GetFinancialYearQueryHandler(IFinancialYearService financialYearService)
        {
            _financialYearService = financialYearService ?? throw new ArgumentNullException(nameof(_financialYearService));
        }
        public Task<List<FinancialDropdownVM>> Handle(GetFinancialYearQuery request, CancellationToken cancellationToken)
        {
            var data = _financialYearService.GetFinancialYear();
            return data;
        }
    }
}
