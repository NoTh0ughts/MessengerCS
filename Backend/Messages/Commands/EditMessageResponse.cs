using System;
using BusinessLogic.Response;
using BusinessLogic.Constants;
using Data.Entity;

namespace Messages.Commands
{
    public record EditMessageResponse : BaseResponce<EditMessageResponse>
    {
        public Message EditedMessage {get;set;}
        public static EditMessageResponse FromSuccess(Message message)
        {
            return new()
            {
                Success = true,
                Message = ResponceMessageCodes.Success,
                EditedMessage = message
            };
        }
    }
}