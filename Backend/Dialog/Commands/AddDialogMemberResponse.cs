using BusinessLogic.Constants;
using BusinessLogic.Response;

namespace Dialog.Commands
{
    public record AddDialogMemberResponse : BaseResponce<AddDialogMemberResponse>
    {
        public static AddDialogMemberResponse FromSuccess() => new()
        {
            Success = true,
            Message = ResponceMessageCodes.Success
        };
    }
}