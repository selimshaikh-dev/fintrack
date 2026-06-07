using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Jamuna.Client_Ledger_Details.ViewModels
{
    public class LedgerListVM
    {
        public DateTime Transaction_Date { get; set; }
        public string Particulars { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Commission { get; set; }
        public decimal Balance { get; set; }
    }
}
