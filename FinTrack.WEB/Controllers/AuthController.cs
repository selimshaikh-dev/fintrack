using FinTrack.Application.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost("emailconfirmation")]
        public async Task<IActionResult> EmailConfirmation(EmailConfirmationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);

        }

        [AllowAnonymous]
        [HttpPost("forgetpassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("setpassword")]
        public async Task<IActionResult> SetPassword(SetPasswordCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
