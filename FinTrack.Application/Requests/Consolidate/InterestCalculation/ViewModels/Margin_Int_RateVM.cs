using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Consolidate.InterestCalculation.ViewModels
{
    public class Margin_Int_RateVM
    {
        public string Client_Code { get; set; }
        public int Client_BP_ID { get; set; }
        public decimal Interest_Rate { get; set; }
    }
}
