using FinTrack.Application.Requests.Consolidate.LedgerDetails.Queries;
using FinTrack.Application.Requests.Consolidate.LedgerSummary.Queries;
using FinTrack.Application.Requests.Consolidate.Portfolio.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsolidatedReportController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ConsolidatedReportController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [AllowAnonymous]
        [HttpGet("getconsolidatedportfolioreport")]
        public async Task<IActionResult> GetConsolidatedPortfolioReport(string MemberID, DateTime EndDate)
        {
            var result = await _mediator.Send(new GetCosolidatedPortfolioReportQuery(MemberID, EndDate));
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("getconsolidatedledgerdetailsreport")]
        public async Task<IActionResult> GetConsolidatedLedgerDetailsReport(string MemberID, DateTime StartDate, DateTime EndDate)
        {
            var result = await _mediator.Send(new GetConsolidateLedgerDetailsQuery(MemberID, StartDate, EndDate));
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("getconsolidatedledgersummaryreport")]
        public async Task<IActionResult> GetConsolidatedLedgerSummaryReport(string MemberID, DateTime StartDate, DateTime EndDate)
        {
            var result = await _mediator.Send(new GetConsolidateLedgerSummaryQuery(MemberID, StartDate, EndDate));
            return Ok(result);
        }
    }
}
