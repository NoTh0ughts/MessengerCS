using System.Linq;
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
    public class AddDialogMemberCommandHandler :
        IRequestHandler<AddDialogMemberCommand, Result<AddDialogMemberResponse>>
    {
        private IUnitOfWork<MessengerContext> unitOfWork;
        private ResponceFactory<AddDialogMemberResponse> responceFactory;

        public AddDialogMemberCommandHandler(IUnitOfWork<MessengerContext> unitOfWork,
            ResponceFactory<AddDialogMemberResponse> responceFactory)
        {
            this.unitOfWork = unitOfWork;
            this.responceFactory = responceFactory;
        }
        public async Task<Result<AddDialogMemberResponse>> Handle(AddDialogMemberCommand request, CancellationToken cancellationToken)
        {
            var invitingUser = await unitOfWork.DbContext.Users.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.InvitingUserId, cancellationToken);
            if (invitingUser is null)
            {
                const string errorMessage = ResponceMessageCodes.UserNotFound;
                var errorDescription = ResponceMessageCodes.ErrorDictionary[errorMessage];

                return responceFactory.ConflictResponce(errorMessage, errorDescription);
            }

            var invitedUser = await unitOfWork.DbContext.Users.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.InvitedUserId, cancellationToken);
            if (invitedUser is null)
            {
                const string errorMessage = ResponceMessageCodes.UserNotFound;
                var errorDescription = ResponceMessageCodes.ErrorDictionary[errorMessage];

                return responceFactory.ConflictResponce(errorMessage, errorDescription);
            }

            var haveAccess = await unitOfWork.DbContext.UserAccesses.AsNoTracking()
                .AnyAsync(x => x.UserId == invitingUser.Id && 
                               x.DialogId == request.DialogId, cancellationToken);

            if (haveAccess == false)
            {
                const string errorMessage = ResponceMessageCodes.PermissionDenied;
                var errorDescription = ResponceMessageCodes.ErrorDictionary[errorMessage];

                return responceFactory.ConflictResponce(errorMessage, errorDescription);
            }

            var alreadyHaveAccess = unitOfWork.DbContext.UserAccesses.AsNoTracking()
                .Any(x => x.DialogId == request.DialogId && x.UserId == request.InvitedUserId);
            if (alreadyHaveAccess == true)
            {
                const string errorMessage = ResponceMessageCodes.UserAlreadyJoinedGroup;
                var errorDescription = ResponceMessageCodes.ErrorDictionary[errorMessage];

                return responceFactory.ConflictResponce(errorMessage, errorDescription);
            }

            var access = new UserAccess
            {
                DialogId = request.DialogId,
                UserId = request.InvitedUserId
            };

            await unitOfWork.DbContext.UserAccesses.AddAsync(access, cancellationToken);
            await unitOfWork.SaveChangesAsync();
            
            return responceFactory.SuccessResponse(AddDialogMemberResponse.FromSuccess());
        }
    }
}