using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Consolidate.Portfolio.ViewModels
{
    public class ClientBalanceVM
    {
        public decimal LB_Jamuna { get; set; }
        public decimal MB_Jamuna { get; set; }
        public decimal MB_Globe { get; set; }
        public decimal LB_Globe { get; set; }
        public decimal LB_Consolidate { get; set; }
        public decimal MB_Consolidate { get; set; }

    }
}
