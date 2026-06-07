using FinTrack.Application.Requests.Jamuna.Menus_Url.Commands;
using FinTrack.Application.Requests.Jamuna.Menus_Url.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenusUrlController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MenusUrlController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [AllowAnonymous]
        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetMenusUrlQuery());
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("getmenusurlbyid")]
        public async Task<IActionResult> GetMenusUrlById(long id)
        {
            var result = await _mediator.Send(new GetMenusUrlByIdQuery(id));
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("getmenusurlbyname")]
        public async Task<IActionResult> GetMenusUrlByName(string name)
        {
            var result = await _mediator.Send(new GetMenusUrlByNameQuery(name));
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("createmenusurl")]
        public async Task<IActionResult> CreateMenusUrl(CreateMenusUrlCommand createMenusUrlCommand)
        {
            var result = await _mediator.Send(createMenusUrlCommand);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPut("updatemenusurl")]
        public async Task<IActionResult> UpdateMenusUrl(UpdateMenusUrlCommand updateMenusUrlCommand)
        {
            var result = await _mediator.Send(updateMenusUrlCommand);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpDelete("deletemenusurl/{id}")]
        public async Task<IActionResult> DeleteMenusUrl(long id)
        {
            var result = await _mediator.Send(new DeleteMenuUrlCommand(id));
            return Ok(result);
        }
    }
}
