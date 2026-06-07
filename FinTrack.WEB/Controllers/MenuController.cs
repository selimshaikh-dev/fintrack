using FinTrack.Application.AuthUser.Commands;
using FinTrack.Application.AuthView.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MenuController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [AllowAnonymous]
        [HttpPost("createmenu")]
        public async Task<IActionResult> CreateMenu(CreateMenuCommand createMenuCommand)
        {
            var result = await _mediator.Send(createMenuCommand);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost("createmenutogroupprivilege")]
        public async Task<IActionResult> CreateMenuToGroupPrivilege(CreateMenuToGroupPrivilegeCommand createMenuToGroupPrivilege)
        {
            var result = await _mediator.Send(createMenuToGroupPrivilege);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost("createmenutouserprivilege")]
        public async Task<IActionResult> CreateMenuToUserPrivilege(CreateMenuToUserPrivilegeCommand createMenuToUserPrivilege)
        {
            var result = await _mediator.Send(createMenuToUserPrivilege);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost("deletemenu")]
        public async Task<IActionResult> DeleteMenu(DeleteMenuCommand deleteMenuCommand)
        {
            var result = await _mediator.Send(deleteMenuCommand);
            return Ok(result);
        }
    }
}
