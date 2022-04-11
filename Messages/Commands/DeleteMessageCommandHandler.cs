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
    public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageComand, Result<DeleteMessageResponce>>
    {
        private IUnitOfWork<MessengerContext> unitOfWork;
        private ResponceFactory<DeleteMessageResponce> responceFactory;


        public DeleteMessageCommandHandler(IUnitOfWork<MessengerContext> unitOfWork, 
        ResponceFactory<DeleteMessageResponce> responceFactory)
        {
            this.unitOfWork = unitOfWork;
            this.responceFactory = responceFactory;
        }


        public async Task<Result<DeleteMessageResponce>> Handle(DeleteMessageComand request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.DbContext.Users.AsNoTracking()
                    .Where(x => x.Id == request.UserId)
                    .FirstOrDefaultAsync();

            if (user is null)
            {
                const string errorMessage = ResponceMessageCodes.UserNotFound;
                var errorDescription = ResponceMessageCodes.ErrorDictionary[errorMessage];

                return responceFactory.ConflictResponce(errorMessage, errorDescription);
            }

            var userAccess = await unitOfWork.DbContext.UserAccesses.AsNoTracking()
                    .Where(x => x.UserId == user.Id && x.DialogId == request.DialogId)
                    .FirstOrDefaultAsync();

            if (userAccess is null)
            {
                const string errorMessage = ResponceMessageCodes.ChatNotFound;
                var errorDescription = ResponceMessageCodes.ErrorDictionary[errorMessage];

                return responceFactory.ConflictResponce(errorMessage, errorDescription);
            }

            var messageToDelete = await unitOfWork.DbContext.Messages.AsNoTracking()
                    .Where(x => x.UserAccessId == userAccess.Id && x.Id == request.MessageId)
                    .FirstOrDefaultAsync();
            if (messageToDelete is null)
            {
                 const string errorMessage = ResponceMessageCodes.MessageNotFound;
                var errorDescription = ResponceMessageCodes.ErrorDictionary[errorMessage];

                return responceFactory.ConflictResponce(errorMessage, errorDescription);
            }

            unitOfWork.DbContext.Messages.Remove(messageToDelete);
            
            await unitOfWork.DbContext.SaveChangesAsync(cancellationToken);

            return responceFactory.SuccessResponse(DeleteMessageResponce.FromSuccess());
        }
    }
}