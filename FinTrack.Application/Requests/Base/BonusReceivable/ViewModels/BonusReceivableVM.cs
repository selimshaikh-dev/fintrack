using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.BonusReceivable.ViewModels
{
    public class BonusReceivableVM
    {
        public Nullable<int> Client_BP_ID { get; set; }
        public string Instrument_ID_DSE { get; set; }
        public Nullable<int> Bounus_Receivable { get; set; }
        public Nullable<System.DateTime> Txn_Date { get; set; }
        public Nullable<System.DateTime> Mature_Date { get; set; }
    }
}
