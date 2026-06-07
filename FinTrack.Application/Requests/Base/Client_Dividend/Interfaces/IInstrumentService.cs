using FinTrack.Application.Requests.Base.Client_Dividend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.Client_Dividend.Interfaces
{
    public interface IInstrumentService : IDisposable
    {
        Task<List<InstrumentVM>> GetAllInstrument();
        Task<bool> CheckInterimDividend(int instrumentId);
    }
}