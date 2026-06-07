using FinTrack.Application.Requests.Jamuna.Client_Ledger_Details.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Consolidate.LedgerDetails.Queries
{
    public class GetConsolidateLedgerDetailsQuery: IRequest<DailyLedgerVM>
    {
        public string MemberID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public GetConsolidateLedgerDetailsQuery(string memberID, DateTime startDate, DateTime endDate)
        {
            MemberID = memberID;
            StartDate = startDate; 
            EndDate = endDate;
        }  
    }
}
