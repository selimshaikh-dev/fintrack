using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Consolidate.InterestCalculation.Interfaces
{
    public interface IInterestCalculationService
    {
        Task<decimal> GetInterest(string clientCode, DateTime txnDate, int clientType, int bpIdgsl);
    }
}
