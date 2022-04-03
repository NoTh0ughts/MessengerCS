using System;
using BusinessLogic.Response;
using BusinessLogic.Constants;


namespace Messages.Commands
{
    public record SendMessageResponce : BaseResponce<SendMessageResponce>
    {
        public int MessageId {get; init;}
        public static SendMessageResponce FromSuccess(int messageId)
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