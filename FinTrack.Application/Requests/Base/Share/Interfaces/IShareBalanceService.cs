using FinTrack.Application.Requests.Base.Share.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.Share.Interfaces
{
    public interface IShareBalanceService
    {
        Task<List<ShareBalanceVM>> GetShareBalances(string clientCode, DateTime endDate);
    }
}
