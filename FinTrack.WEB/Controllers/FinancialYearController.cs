using FinTrack.Application.Requests.Jamuna.Financial_Year.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialYearController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FinancialYearController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [AllowAnonymous]
        [HttpGet("getfinancialyear")]
        public async Task<IActionResult> GetFinancialYear()
        {
            var result = await _mediator.Send(new GetFinancialYearQuery());
            return Ok(result);
        }
    }
}
