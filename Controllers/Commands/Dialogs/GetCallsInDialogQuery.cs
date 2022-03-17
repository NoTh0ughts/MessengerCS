using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Messenger.Controllers.Commands.Bot.BotCommand;
using Messenger.DB.entity;
using Messenger.Host.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Controllers.Commands.Dialogs
{
    public class GetCallsInDialogQuery : IRequest<IEnumerable<Call>>
    {
        public int DialogId { get; set; }
        
        public class GetCallsInDialogQueryHandler : IRequestHandler<GetCallsInDialogQuery, IEnumerable<Call>>
        {
            private readonly IUnitOfWork<MessengerContext> _unitOfWork;

            public GetCallsInDialogQueryHandler(IUnitOfWork<MessengerContext> unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<Call>> Handle(GetCallsInDialogQuery request, CancellationToken cancellationToken)
            {
                if (request.DialogId > 0)
                {
                    return await _unitOfWork.DbContext.Calls
                        .Where(c => c.DialogId == request.DialogId)
                        .ToListAsync(cancellationToken);
                }

                return null;
            }
        }

    }
}