using System;
using Data.UnitOfWork;
using MediatR;
using BusinessLogic.Response;
using System.Threading.Tasks;
using System.Threading;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BusinessLogic.Constants;

namespace Messages.Commands 
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, Result<SendMessageResponse>>
    {
        private IUnitOfWork<MessengerContext> unitOfWork;
        private ResponseFactory<SendMessageResponse> _responseFactory;

        public SendMessageCommandHandler(IUnitOfWork<MessengerContext> unitOfWork, ResponseFactory<SendMessageResponse> responseFactory)
        {
            this.unitOfWork = unitOfWork;
            this._responseFactory = responseFactory;
        }

        public async Task<Result<SendMessageResponse>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.DbContext.Users.AsNoTracking()
                    .Select(x => new 
                    {
                        x.Name,
                        x.Id
                    }).FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
            
            if (user is null)
            {
                const string errorMessage = ResponceMessageCodes.UserNotFound;
                var errorDescription = ResponceMessageCodes.ErrorDictionary[errorMessage];

                return _responseFactory.ConflictResponce(errorMessage, errorDescription);
            }

            var userAccess = await unitOfWork.DbContext.UserAccesses.AsNoTracking()
                .Select(x => new 
                {
                    x.Dialog,
                    x.UserId,
                    x.Id
                }).FirstOrDefaultAsync(x => x.Dialog.Id == request.DialogId, cancellationToken: cancellationToken);

            if(userAccess is null)
            {
                const string errorMessage = ResponceMessageCodes.ChatNotFound;
                var errorDescription = ResponceMessageCodes.ErrorDictionary[errorMessage];

                return _responseFactory.ConflictResponce(errorMessage, errorDescription);
            }

            var message = new Message
            {
                TextMessage = request.TextMessage,
                UserAccessId = userAccess.Id,
                SendDate = DateTime.Now,
            };

            // TODO: MB need add updatedAt to dialog table

            var entry = await unitOfWork.DbContext.Messages.AddAsync(message, cancellationToken);

            await unitOfWork.DbContext.SaveChangesAsync(cancellationToken);
            
            return _responseFactory.SuccessResponse(SendMessageResponse.FromSuccess(message.Id));
        }
    }
}