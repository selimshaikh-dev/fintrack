using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Consolidate.InterestCalculation.ViewModels
{
    public class Client_BPID_InterestAmount
    {
        public int BP_ID { get; set; }
        public decimal Interest_Amount { get; set; }
    }
}
