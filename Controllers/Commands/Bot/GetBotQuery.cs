using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Messenger.DB.entity;
using Messenger.Host.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Controllers.Commands.Bot
{
    public class GetBotQuery : IRequest<IEnumerable<DB.entity.Bot>>
    {
        public int BotID { get; set; }
        
        public class GetBotQueryHandler : IRequestHandler<GetBotQuery, IEnumerable<DB.entity.Bot>>
        {
            private readonly IUnitOfWork<MessengerContext> _unitOfWork;
            
            public GetBotQueryHandler(IUnitOfWork<MessengerContext> unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }
            
            public async Task<IEnumerable<DB.entity.Bot>> Handle(GetBotQuery request, CancellationToken cancellationToken)
            {
                if (request.BotID != 0)
                {
                    return await _unitOfWork.DbContext.Bots.Where(x => x.Id == request.BotID)
                        .ToListAsync(cancellationToken);
                }

                return await _unitOfWork.DbContext.Bots.OrderBy(x => x.IdNavigation.Name)
                    .Take(10)
                    .ToListAsync(cancellationToken);
            }
        }
        
    }
}