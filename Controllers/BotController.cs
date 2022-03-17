using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Messenger.Controllers.Commands;
using Messenger.Controllers.Commands.Bot;
using Messenger.Controllers.Commands.Bot.BotCommand;
using Messenger.DB.entity;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Controllers
{
    public class BotController : Controller
    {
        private readonly IMediator _mediator;

        public BotController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("bot/{botId}")]
        [ProducesResponseType(typeof(Bot), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Get(int botId)
        {
            var bots = await _mediator.Send(new GetBotQuery {BotID = botId});

            return bots.Count() switch
            {
                > 1 => Ok(bots),
                1 =>   Ok(bots.FirstOrDefault()),
                _ =>   NotFound()
            };
        }

        [HttpGet("bot/{botId}/commands/{limitCount?}")]
        [ProducesResponseType(typeof(Command), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCommands(int botId, int limitCount = 20)
        {
            var commands = await _mediator.Send(new GetBotCommandsQuery {BotId = botId, LimitCount = limitCount});
            if (commands is null)
            {
                return NotFound();
            }

            return Ok(commands);
        }
    }
}