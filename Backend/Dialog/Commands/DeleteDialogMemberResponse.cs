using BusinessLogic.Constants;
using BusinessLogic.Response;

namespace Dialog.Commands
{
    public record DeleteDialogMemberResponse : BaseResponce<DeleteDialogMemberResponse>
    {
        public static DeleteDialogMemberResponse FromSuccess() => new()
        {
            Success = true,
            Message = ResponceMessageCodes.Success
        };
    }
}