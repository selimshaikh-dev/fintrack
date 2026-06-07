using FinTrack.Application.AuthView.Commands;
using FinTrack.Application.Commands.Base.ClientCashDividend;
using FinTrack.Application.Requests.Base.Client_Dividend.Queries;
using FinTrack.Application.Requests.Jamuna.Client_Ledger_Details.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashDividendController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CashDividendController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [AllowAnonymous]
        [HttpGet("getclientbalanceinfo")]
        public async Task<IActionResult> GetClientBalanceDetails(string clientCode)
        {
            var result = await _mediator.Send(new GetClientBalanceQuery(clientCode));
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("getallinstrument")]
        public async Task<IActionResult> GetInstrumentList()
        {
            var instrumentList = await _mediator.Send(new GetAllInstrumentsQuery());
            return Ok(instrumentList);
        }

        [AllowAnonymous]
        [HttpGet("getclientwisecashdividend")]
        public async Task<IActionResult> GetClientwiseCashDividend(string clientCode, int instrumentId)
        {
            var clientCashDividend = await _mediator.Send(new GetClientwiseCashDividendQuery(clientCode,instrumentId));
            return Ok(clientCashDividend);
        }

        [AllowAnonymous]
        [HttpPost("savecashdividend")]
        public async Task<IActionResult> SaveCashDividend(AddCashDividendCommand addCashDividendCommand)
        {
            var result = await _mediator.Send(addCashDividendCommand);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("checkinterimdividend")]
        public async Task<IActionResult> CheckInterimDividend(int instrumentId)
        {
            var result = await _mediator.Send(new CheckInterimDividendQuery(instrumentId));
            return Ok(result); 
        }
    }
}