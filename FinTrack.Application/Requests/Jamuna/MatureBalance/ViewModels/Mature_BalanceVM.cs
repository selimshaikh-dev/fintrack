using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.MatureBalance.ViewModels
{
    public class Mature_BalanceVM
    {
        public string Client_Code { get; set; }
        public int Client_BP_ID { get; set; }
        public decimal Mature_Balance { get; set; }
    }
}
