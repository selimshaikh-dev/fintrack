using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Consolidate.Portfolio.ViewModels
{
    public class ClientDetailsVM
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string LastTranDate { get; set; }
        public string AccountStatus { get; set; }
        public string JamunaMemberID { get; set; }
        public string Type_Name_Marketing { get; set; }
        public string AsOn { get; set; }
    }
}
