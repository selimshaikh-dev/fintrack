using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Consolidate.InterestCalculation.Interfaces
{
    public interface IAccruedInterestDateService
    {
        Task<DateTime> GetInterestDateAsync(string clientCode, DateTime transactionDate);
    }
}
