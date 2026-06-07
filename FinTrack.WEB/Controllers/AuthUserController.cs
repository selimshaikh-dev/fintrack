using FinTrack.Application.AuthRole.Commands;
using FinTrack.Application.AuthRole.Queries;
using FinTrack.Application.AuthUser.Commands;
using FinTrack.Application.AuthUser.Queries;
using FinTrack.Application.AuthUser.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthUserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthUserController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        [AllowAnonymous]
        [HttpGet("getusers")]
        public async Task<IActionResult> GetUsers(int PageNumber, int PageSize)
        {
            var result = await _mediator.Send(new GetUserQuery(PageNumber,PageSize));
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("getmembers")]
        public async Task<IActionResult> GetMembers(int PageNumber, int PageSize)
        {
            var result = await _mediator.Send(new GetMemberQuery(PageNumber, PageSize));
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("getuserbyid")]
        public async Task<IActionResult> GetUserById(string Id)
        {
            var result = await _mediator.Send(new GetUserByIdCommand(Id));
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("getuserbyemail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var result = await _mediator.Send(new GetUserByEmailCommand(email));
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("createuser")]
        public async Task<IActionResult> CreateUser(CreateUserCommand createUserCommand)
        {
            var result = await _mediator.Send(createUserCommand);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPut("updateuser")]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand updateUserCommand)
        {
            var result = await _mediator.Send(updateUserCommand);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpDelete("deleteuser/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _mediator.Send(new DeleteUserCommand(id));
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPut("activeordeactiveuser")]
        public async Task<IActionResult> ActiveOrDeactiveUser(ActiveOrDeactiveUserCommand activeOrDeactiveUserCommand)
        {
            var result = await _mediator.Send(activeOrDeactiveUserCommand);
            return Ok(result);
        }
    }
}
