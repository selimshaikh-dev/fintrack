using FinTrack.Application.Requests.Jamuna.MatureBalance.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.MatureBalance.Interfaces
{
    public interface IMatureBalanceServiceJSCCL 
    {
        Task<List<Mature_BalanceVM>> GetMatureBalanceJSCCLMulti(DateTime matureDate, DataTable dtClient);
    }
}
