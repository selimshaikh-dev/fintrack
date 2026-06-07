using FinTrack.Application.AuthView.Models;
using FinTrack.Application.Common.Models;
using FinTrack.Application.Requests.Base.Client_Dividend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Commands.Base.Interfaces
{
    public interface IAddCashDividendService : IDisposable
    {
        Task<Result> AddCashDividend(CashDividendVM CashDividend);
    }
}