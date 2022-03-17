using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Messenger.DB.entity;
using Messenger.Host.UnitOfWork;

namespace Messenger.Controllers.Commands.Dialogs
{
    public class GetUserMessagesInDialogQuery : IRequest<IEnumerable<Message>>
    {
        public int DialogId { get; set; }
        public int UserId { get; set; }
        public int LimitCount { get; set; }

        public class GetUserMessagesInDialogQueryHandler : IRequestHandler<GetUserMessagesInDialogQuery, IEnumerable<Message>>
        {
            private readonly IUnitOfWork<MessengerContext> _unitOfWork;

            public GetUserMessagesInDialogQueryHandler(IUnitOfWork<MessengerContext> unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<Message>> Handle(GetUserMessagesInDialogQuery request, CancellationToken cancellationToken)
            {
                if (request.UserId > 0 && request.DialogId > 0)
                {
                    return _unitOfWork.DbContext.UserAccesses
                        .First(ud => ud.DialogId == request.DialogId && ud.UserId == request.UserId)
                        .Messages
                        .AsEnumerable();
                }

                return null;
            }
        }
    }
}