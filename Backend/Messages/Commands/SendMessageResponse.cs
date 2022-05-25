using BusinessLogic.Response;
using BusinessLogic.Constants;


namespace Messages.Commands
{
    public record SendMessageResponse : BaseResponce<SendMessageResponse>
    {
        public int MessageId {get; init;}
        public static SendMessageResponse FromSuccess(int messageId)
        {
            return new()
            {
                Success = true,
                Message = ResponceMessageCodes.Success,
                MessageId = messageId
            };
        }
    }
}