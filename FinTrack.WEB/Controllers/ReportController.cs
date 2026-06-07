using FinTrack.Application.Requests.Jamuna.Client_Ledger_Details.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReportController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [AllowAnonymous]
        [HttpGet("getclientledgerdetails")]
        public async Task<IActionResult> GetClientLedgerDetails(string MemberID, DateTime StartDate, DateTime EndDate)
        {
            var result = await _mediator.Send(new GetClientLedgerDetailsQuery(MemberID, StartDate, EndDate));
            return Ok(result);
        }
    }
}
