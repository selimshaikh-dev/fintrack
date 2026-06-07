using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Consolidate.InterestCalculation.ViewModels
{
    public class Daily_Activity_BalVM
    {
        public string Client_Code { get; set; }
        public int Client_BP_ID { get; set; }
        public DateTime Mature_Date { get; set; }
        public decimal Amount { get; set; }
    }
}
