using FinTrack.Application.Requests.Jamuna.Financial_Year.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.Financial_Year.Queries
{
    public class GetFinancialYearQuery: IRequest<List<FinancialDropdownVM>>
    {
        public GetFinancialYearQuery()
        {
                
        }
    }
}
