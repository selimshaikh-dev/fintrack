using FinTrack.Application.Requests.Base.BonusReceivable.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.BonusReceivable.Interfaces
{
    public interface IBonusReceivableService
    {
        Task<List<BonusReceivableVM>> GetBonusReceivableInfo(string clientCode, DateTime endDate);
    }
}
