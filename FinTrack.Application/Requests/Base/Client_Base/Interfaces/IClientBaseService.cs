using FinTrack.Application.Requests.Base.Client_Base.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.Client_Base.Interfaces
{
    public interface IClientBaseService: IDisposable
    {
        public Task<Client_InfosVM> GetClientInfos(string clientCode);
    }
}
