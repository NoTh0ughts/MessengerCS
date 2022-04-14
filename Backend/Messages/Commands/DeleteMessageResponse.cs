using BusinessLogic.Response;
using BusinessLogic.Constants;

namespace Messages.Commands
{
    public record DeleteMessageResponse : BaseResponce<DeleteMessageResponse>
    {
        public static DeleteMessageResponse FromSuccess() => new()
        {
            Success = true,
            Message = ResponceMessageCodes.Success
        };
    }
}