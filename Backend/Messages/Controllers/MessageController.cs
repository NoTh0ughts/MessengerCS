using System.Threading.Tasks;
using BusinessLogic.Controllers;
using BusinessLogic.Response;
using MediatR;
using Messages.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Messages.Controllers
{
    [Authorize]
    [Route("api/message")]
    public class MessageController : ApiControllerBase
    {
        public MessageController(IMediator mediator) : base(mediator) {}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SendMessageResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponce))]
        public async Task<IActionResult> SendMessage(int dialogId, string textMessage)
        {
            SendMessageCommand request = new SendMessageCommand
            {
                DialogId = dialogId,
                TextMessage = textMessage,
                UserId = HttpContext.User.GetUserId()
            };

            return await RequestAsync(request, default);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteMessageComand))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponce))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponce))]
        public async Task<IActionResult> DeleteMessage(int messageId, int dialogId)
        {
            DeleteMessageComand request = new DeleteMessageComand
            {
                DialogId = dialogId,
                MessageId = messageId,
                UserId = HttpContext.User.GetUserId()
            };

            return await RequestAsync(request, default);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EditMessageCommand))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponce))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponce))]
        public async Task<IActionResult> EditMessage(int messageId, int dialogId, string newText)
        {
            EditMessageCommand request = new EditMessageCommand 
            {
                MessageId = messageId,
                NewText = newText, 
                UserId = HttpContext.User.GetUserId()
            };
            
            return await RequestAsync(request, default);
        }
    }
}