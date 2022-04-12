using System;
using BusinessLogic.Response;
using BusinessLogic.Constants;
using Data.Entity;

namespace Messages.Commands
{
    public record EditMessageResponce : BaseResponce<EditMessageResponce>
    {
        public Message EditedMessage {get;set;}
        public static EditMessageResponce FromSuccess(Message message)
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