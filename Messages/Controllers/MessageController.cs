using System;
using System.Threading.Tasks;
using BusinessLogic.Response;
using Data.Entity;
using Data.UnitOfWork;
using MediatR;
using Messages.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Messages.Controllers
{
    public class MessageController : Controller
    {
        private readonly IUnitOfWork<MessengerContext> unitOfWork;
        private readonly IMediator mediator;

        public MessageController(IUnitOfWork<MessengerContext> unitOfWork, IMediator mediator)
        {
            this.unitOfWork = unitOfWork;
            this.mediator = mediator;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SendMessageResponce))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponce))]
        public async Task<IActionResult> SendMessage(int dialogId, string textMessage)
        {
            SendMessageCommand request = new SendMessageCommand
            {
                DialogId = dialogId,
                TextMessage = textMessage,
                UserId = HttpContext.User.GetUserId()
            };

            return 
        }
    }
}