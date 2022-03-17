using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Messenger.DB.entity;
using Messenger.Host.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Controllers.Commands.Bot.BotCommand
{
    public class GetBotCommandsQuery : IRequest<IEnumerable<Command>>
    {
        public int BotId { get; set; }
        public int LimitCount { get; set; }
        
        public class GetBotCommandsQueryHandler : IRequestHandler<GetBotCommandsQuery, IEnumerable<Command>>
        {
            private readonly IUnitOfWork<MessengerContext> _unitOfWork;

            public GetBotCommandsQueryHandler(IUnitOfWork<MessengerContext> unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<Command>> Handle(GetBotCommandsQuery request, CancellationToken cancellationToken)
            {
                if (request.BotId > 0)
                {
                    return await _unitOfWork.DbContext.Commands
                        .Where(x => x.IdBot == request.BotId)
                        .Take(request.LimitCount)
                        .ToListAsync(cancellationToken);
                }

                return null;
            }
        }
    }
}