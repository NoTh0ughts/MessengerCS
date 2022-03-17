using System.Threading.Tasks;
using MediatR;
using Messenger.Controllers.Commands.Dialogs;
using Messenger.DB.entity;
using Microsoft.AspNetCore.Mvc;

namespace Messenger.Controllers
{
    public class DialogController : Controller
    {
        private readonly IMediator _mediator;

        public DialogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("dialog/{dialogId}/allUsedStickers/{limitCount?}")]
        [ProducesResponseType(typeof(Sticker), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUsedStickersInDialog(int dialogId, int limitCount = 20)
        {
            var stickers = await _mediator.Send(new GetUsedStickersInDialogQuery
            {
                DialogId = dialogId,
                LimitCount = limitCount
            });

            if (stickers is null)
                return NotFound();

            return Ok(stickers);
        }

        [HttpGet("dialog/{dialogId}/users")]
        [ProducesResponseType(typeof(Sticker), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUsersInDialog(int dialogId)
        {
            var users = await _mediator.Send(new GetUsersInDialogQuery {DialogId = dialogId});
            
            if (users is null)
                return NotFound();

            return Ok(users);
        }
        
        [HttpGet("dialog/{dialogId}/calls")]
        [ProducesResponseType(typeof(Call), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetCallsInDialog(int dialogId)
        {
            var calls = await _mediator.Send(new GetCallsInDialogQuery {DialogId = dialogId});
            
            if (calls is null)
                return NotFound();

            return Ok(calls);
        }
        
        [HttpGet("dialog/{dialogId}/messages/{userId}/{limitCount}")]
        [ProducesResponseType(typeof(Message), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetUserMessagesInDialog(int dialogId, int userId, int limitCount = 30)
        {
            var messages = await _mediator.Send(new GetUserMessagesInDialogQuery
            {
                DialogId = dialogId,
                LimitCount = limitCount,
                UserId = userId
            });
            
            if (messages is null)
                return NotFound();

            return Ok(messages);
        }
        
    }
}