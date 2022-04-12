using BusinessLogic.Response;
using BusinessLogic.Constants;

namespace Messages.Commands
{
    public record DeleteMessageResponce : BaseResponce<DeleteMessageResponce>
    {
        public static DeleteMessageResponce FromSuccess() => new()
        {
            Success = true,
            Message = ResponceMessageCodes.Success
        };
    }
}