using System.Threading.Tasks;
using MediatR;
using Messenger.Controllers.Commands.Settings;
using Messenger.DB.entity;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Controllers
{
    public class SettingsController : Controller
    {
        private readonly IMediator _mediator;

        public SettingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpGet("settings/{paramName}/allUsers")]
        [ProducesResponseType(typeof(User), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUsersWithParameter(string paramName)
        {
            var users = await _mediator.Send(new GetUsersWithParameterQuery {ParamName = paramName,LimitCount = 10});
            
            if (users is not null) return Ok(users);

            return NotFound();
        }
    }
}