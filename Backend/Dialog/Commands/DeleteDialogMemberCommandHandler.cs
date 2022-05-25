using System.Threading;
using System.Threading.Tasks;
using BusinessLogic.Constants;
using BusinessLogic.Response;
using Data.Entity;
using Data.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dialog.Commands
{
    public class DeleteDialogMemberCommandHandler :
        IRequestHandler<DeleteDialogMemberCommand, Result<DeleteDialogMemberResponse>>
    {
        private IUnitOfWork<MessengerContext> unitOfWork;
        private ResponseFactory<DeleteDialogMemberResponse> _responseFactory;

        
        public DeleteDialogMemberCommandHandler
            (IUnitOfWork<MessengerContext> unitOfWork, ResponseFactory<DeleteDialogMemberResponse> responseFactory)
        {
            this.unitOfWork = unitOfWork;
            this._responseFactory = responseFactory;
        }
        
        public async Task<Result<DeleteDialogMemberResponse>> Handle
            (DeleteDialogMemberCommand request, CancellationToken cancellationToken)
        {
             var deletingUser = await unitOfWork.DbContext.Users.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.DeletingUserId, cancellationToken);
            if (deletingUser is null)
            {
                const string errorMessage = ResponceMessageCodes.UserNotFound;
                var errorDescription = ResponceMessageCodes.ErrorDictionary[errorMessage];

                return _responseFactory.ConflictResponce(errorMessage, errorDescription);
            }

            var deletedUser = await unitOfWork.DbContext.Users.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.DeletedUserId, cancellationToken);
            if (deletedUser is null)
            {
                const string errorMessage = ResponceMessageCodes.UserNotFound;
                var errorDescription = ResponceMessageCodes.ErrorDictionary[errorMessage];

                return _responseFactory.ConflictResponce(errorMessage, errorDescription);
            }

            var haveAccess = await unitOfWork.DbContext.UserAccesses.AsNoTracking()
                .AnyAsync(x => x.UserId == deletingUser.Id && 
                               x.DialogId == request.DialogId, cancellationToken);

            if (haveAccess == false)
            {
                const string errorMessage = ResponceMessageCodes.PermissionDenied;
                var errorDescription = ResponceMessageCodes.ErrorDictionary[errorMessage];

                return _responseFactory.ConflictResponce(errorMessage, errorDescription);
            }

            var access = await unitOfWork.DbContext.UserAccesses.AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == request.DeletedUserId && 
                                          x.DialogId == request.DialogId, cancellationToken);

            if (access is null)
            {
                const string errorMessage = ResponceMessageCodes.ChatNotFound;
                var errorDescription = ResponceMessageCodes.ErrorDictionary[errorMessage];

                return _responseFactory.ConflictResponce(errorMessage, errorDescription);
            }

            unitOfWork.DbContext.UserAccesses.Remove(access);
            
            return _responseFactory.SuccessResponse(DeleteDialogMemberResponse.FromSuccess());
        }
    }
}