using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Consolidate.Portfolio.ViewModels
{
    public class CapitalGainLossVM
    {
        public decimal? RealisedGain { get; set; }
        public decimal? UnrealisedGain { get; set; }        
        public decimal? TotalGainOrLoss { get; set; }
    }
}
