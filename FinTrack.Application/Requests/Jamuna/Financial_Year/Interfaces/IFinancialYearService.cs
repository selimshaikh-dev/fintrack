using FinTrack.Application.Requests.Jamuna.Financial_Year.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.Financial_Year.Interfaces
{
    public interface IFinancialYearService
    {
        Task<List<FinancialDropdownVM>> GetFinancialYear();
    }
}
