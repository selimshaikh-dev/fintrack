using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Base.Share.ViewModels
{
    public class ShareBalanceVM
    {
        public string Client_Code { get; set; }
        public string Client_Name { get; set; }
        public string Bo_Id_DSE { get; set; }
        public Nullable<int> Instrument_ID { get; set; }
        public Nullable<int> Total_Quantity { get; set; }
        public Nullable<int> Free_Quantity { get; set; }
        public Nullable<int> Pledge_Quantity { get; set; }
        public Nullable<decimal> Avg_Rate { get; set; }
        public decimal Market_price { get; set; }
        public string Instrument_Id_DSE { get; set; }
        public string Category { get; set; }
        public Nullable<bool> Is_Marginable_Securities { get; set; }
    }
}
