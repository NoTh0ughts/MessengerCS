using System;
using System.Threading.Tasks;
using Data.Entity;
using Data.UnitOfWork;
using MediatR;
using Messages.Commands;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IAsyncResult> SendMessage(int dialogId, string textMessage)
        {    
            var messageId = await mediator.Send(new SendMessageCommand {
                

            });


            return null;
        }
    }
}