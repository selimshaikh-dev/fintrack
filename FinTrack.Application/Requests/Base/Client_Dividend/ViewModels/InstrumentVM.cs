using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.Client_Dividend.ViewModels
{
    public class InstrumentVM
    {
        public int Instrument_ID { get; set; }
        public string Instrument_ID_DSE { get; set; }
        public string Message { get; set; }
    }
}