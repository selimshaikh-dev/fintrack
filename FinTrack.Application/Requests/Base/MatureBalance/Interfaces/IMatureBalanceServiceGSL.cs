using FinTrack.Application.Requests.Jamuna.MatureBalance.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.MatureBalance.Interfaces
{
    public interface IMatureBalanceServiceGSL
    {
        Task<List<Mature_BalanceVM>> GetMatureBalanceGSLMulti(DateTime matureDate, DataTable dtClient);
    }
}
