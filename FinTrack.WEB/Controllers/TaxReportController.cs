using FinTrack.Application.Requests.Jamuna.TaxReport.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxReportController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TaxReportController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [AllowAnonymous]
        [HttpGet("gettaxcertificatereport")]
        public async Task<IActionResult> GetTaxCertificateReport(string MemberID, string FinancialYear)
        {
            var result = await _mediator.Send(new TaxCertificateQuery(MemberID, FinancialYear));
            return Ok(result);
        }
    }
}
