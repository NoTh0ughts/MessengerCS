using System;
using MediatR;
using BusinessLogic.Response;

namespace Messages.Commands
{
    public class SendMessageCommand : IRequest<Result<SendMessageResponse>>
    {
        public int UserId {get;set;}
        public string TextMessage {get;set;}
        public int DialogId {get;set;}
    }
}