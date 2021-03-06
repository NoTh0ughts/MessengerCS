using Data.UnitOfWork;
using MediatR;
using BusinessLogic.Response;
using System.Threading.Tasks;
using System.Threading;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using BusinessLogic.Constants;

namespace Messages.Commands
{
    public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageComand, Result<DeleteMessageResponse>>
    {
        private IUnitOfWork<MessengerContext> unitOfWork;
        private ResponseFactory<DeleteMessageResponse> _responseFactory;


        public DeleteMessageCommandHandler(IUnitOfWork<MessengerContext> unitOfWork, 
        ResponseFactory<DeleteMessageResponse> responseFactory)
        {
            this.unitOfWork = unitOfWork;
            this._responseFactory = responseFactory;
        }


        public async Task<Result<DeleteMessageResponse>> Handle(DeleteMessageComand request, CancellationToken cancellationToken)
        {
            var user = await unitOfWork.DbContext.Users.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.UserId);

            if (user is null)
            {
                const string errorMessage = ResponceMessageCodes.UserNotFound;
                var errorDescription = ResponceMessageCodes.ErrorDictionary[errorMessage];

                return _responseFactory.ConflictResponce(errorMessage, errorDescription);
            }

            var userAccess = await unitOfWork.DbContext.UserAccesses.AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == user.Id && x.DialogId == request.DialogId);

            if (userAccess is null)
            {
                const string errorMessage = ResponceMessageCodes.ChatNotFound;
                var errorDescription = ResponceMessageCodes.ErrorDictionary[errorMessage];

                return _responseFactory.ConflictResponce(errorMessage, errorDescription);
            }

            var messageToDelete = await unitOfWork.DbContext.Messages.AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserAccessId == userAccess.Id && x.Id == request.MessageId);

            if (messageToDelete is null)
            {
                const string errorMessage = ResponceMessageCodes.MessageNotFound;
                var errorDescription = ResponceMessageCodes.ErrorDictionary[errorMessage];

                return _responseFactory.ConflictResponce(errorMessage, errorDescription);
            }

            unitOfWork.DbContext.Messages.Remove(messageToDelete);
            
            await unitOfWork.DbContext.SaveChangesAsync(cancellationToken);

            return _responseFactory.SuccessResponse(DeleteMessageResponse.FromSuccess());
        }
    }
}