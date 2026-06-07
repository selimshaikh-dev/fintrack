using FinTrack.Application.Requests.Consolidate.Purchase_Power.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Consolidate.Purchase_Power.Interfaces
{
    public interface IConsolidatedPurchasePowerService
    {
        Task<PurchasePowerVM> GetConsolidatedPurchasePower(string ClientCode, DateTime TransactionDate);
    }
}
