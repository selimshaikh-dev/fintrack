using FinTrack.Application.Requests.Base.Ipo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.Ipo.Interfaces
{
    public interface IIpoApplicationService
    {
        Task<List<IpoApplicationVM>> GetIpoApplicationInfo(int bpID);
    }
}
