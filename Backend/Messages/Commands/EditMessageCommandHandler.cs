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
    public class EditMessageCommandHandler : IRequestHandler<EditMessageCommand, Result<EditMessageResponce>>
    {
        private IUnitOfWork<MessengerContext> unitOfWork;
        private ResponceFactory<EditMessageResponce> responceFactory;

        public EditMessageCommandHandler(IUnitOfWork<MessengerContext> unitOfWork, ResponceFactory<EditMessageResponce> responceFactory)
        {
            this.unitOfWork = unitOfWork;
            this.responceFactory = responceFactory;
        }

        public async Task<Result<EditMessageResponce>> Handle(EditMessageCommand request, CancellationToken cancellationToken)
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

                return responceFactory.ConflictResponce(errorMessage, errorDescription);
            }

            var message = await unitOfWork.DbContext.Messages.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.MessageId);
            if (message is null)
            {
                const string errorMessage = ResponceMessageCodes.MessageNotFound;
                var errorDescription = ResponceMessageCodes.ErrorDictionary[errorMessage];

                return responceFactory.ConflictResponce(errorMessage, errorDescription);
            }
            
            var userAccess = await unitOfWork.DbContext.UserAccesses.AsNoTracking()
                .Select(x => new
                {
                    x.UserId,
                    x.Id
                }).FirstOrDefaultAsync(x => x.UserId == request.UserId && x.Id == message.UserAccessId);

            if(userAccess is null)
            {
                const string errorMessage = ResponceMessageCodes.ChatNotFound;
                var errorDescription = ResponceMessageCodes.ErrorDictionary[errorMessage];

                return responceFactory.ConflictResponce(errorMessage, errorDescription);
            }

            message.TextMessage = request.NewText;
            unitOfWork.DbContext.Messages.Update(message);

            return responceFactory.SuccessResponse(EditMessageResponce.FromSuccess(message));
        }
    }
}