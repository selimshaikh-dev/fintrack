using FinTrack.Application.Requests.Jamuna.Client_Jamuna.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.Client_Jamuna.Interfaces
{
    public interface IClientServiceJamuna: IDisposable
    {
        public Task<ClientInfosJamunaVM> GetClientInfosByEmail(string email);
        public Task<ClientInfosJamunaVM> GetClientInfoInPlutoByEmail(string email);
        public Task<ClientInfosJamunaVM> GetClientInfosJamuna(string clientCode);
    }
}
