using FinTrack.Application.AuthRole.Commands;
using FinTrack.Application.AuthRole.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthRoleController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthRoleController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [AllowAnonymous]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetRoleQuery());
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("getrolebyid")]
        public async Task<IActionResult> GetRoleById(string id)
        {
            var result = await _mediator.Send(new GetRoleByIdQuery(id));
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("getrolebyname")]
        public async Task<IActionResult> GetRoleByName(string name)
        {
            var result = await _mediator.Send(new GetRoleByNameQuery(name));
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("createrole")]
        public async Task<IActionResult> CreateRole(CreateRoleCommand createRoleCommand)
        {
            var result = await _mediator.Send(createRoleCommand);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPut("updaterole")]
        public async Task<IActionResult> UpdateRole(UpdateRoleCommand updateRoleCommand)
        {
            var result = await _mediator.Send(updateRoleCommand);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpDelete("deleterole/{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var result = await _mediator.Send(new DeleteRoleCommand(id));
            return Ok(result);
        }
    }
}
