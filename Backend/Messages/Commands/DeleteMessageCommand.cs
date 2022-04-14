using System;
using MediatR;
using BusinessLogic.Response;

namespace Messages.Commands
{
    public class DeleteMessageComand : IRequest<Result<DeleteMessageResponse>>
    {
        public int UserId {get;set;}
        public int MessageId {get;set;}
        public int DialogId {get;set;}
    }
}