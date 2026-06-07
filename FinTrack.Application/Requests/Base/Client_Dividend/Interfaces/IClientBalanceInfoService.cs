using FinTrack.Application.Requests.Base.Client_Dividend.ViewModels;

namespace FinTrack.Application.Requests.Base.Client_Dividend.Interfaces
{
    public interface IClientBalanceInfoService: IDisposable
    {
        Task<ClientBalanceInfoVM> GetClientBalanceInfo(string clientCode);
    }
}