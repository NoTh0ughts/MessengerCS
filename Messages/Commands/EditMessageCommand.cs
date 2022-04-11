using System;
using MediatR;
using BusinessLogic.Response;

namespace Messages.Commands
{
    public class EditMessageCommand : IRequest<Result<EditMessageResponce>>
    {
        public int UserId {get;set;}
        public string NewText {get;set;}
        public int MessageId {get;set;}
    }
}
