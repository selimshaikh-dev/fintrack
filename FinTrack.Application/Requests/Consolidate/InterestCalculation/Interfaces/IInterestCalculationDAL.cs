using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Consolidate.InterestCalculation.Interfaces
{
    public interface IInterestCalculationDAL
    {
        Task<DataTable> Client_BPID_Code_For_JSCCL(string clientCode);
    }
}
