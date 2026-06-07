using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.Ipo.ViewModels
{
    public class IpoApplicationVM
    {
        public decimal IPO_Rate { get; set; }
        public decimal App_Amount { get; set; }
        public int Quantity { get; set; }
        public string Instrument_ID_DSE { get; set; }
    }
}
