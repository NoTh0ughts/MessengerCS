using BusinessLogic.Constants;
using BusinessLogic.Response;

namespace Dialog.Commands
{
    public record AddDialogMemberResponce : BaseResponce<AddDialogMemberResponce>
    {
        public static AddDialogMemberResponce FromSuccess() => new()
        {
            Success = true,
            Message = ResponceMessageCodes.Success
        };
    }
}