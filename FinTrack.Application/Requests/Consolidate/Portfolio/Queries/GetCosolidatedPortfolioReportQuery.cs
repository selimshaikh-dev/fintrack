using FinTrack.Application.Requests.Consolidate.Portfolio.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Application.Requests.Consolidate.Portfolio.Queries
{
    public class GetCosolidatedPortfolioReportQuery: IRequest<CosolidatedPortfolioVM>
    {
        public string MemberID { get; set; }
        public DateTime EndDate { get; set; }
        public GetCosolidatedPortfolioReportQuery(string memberID, DateTime endDate)
        {
            MemberID = memberID;
            EndDate = endDate;               
        }
    }
}
