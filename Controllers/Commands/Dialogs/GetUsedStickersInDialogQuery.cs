using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Messenger.DB.entity;
using Messenger.Host.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Controllers.Commands.Dialogs
{
    public class GetUsedStickersInDialogQuery : IRequest<IEnumerable<Sticker>>
    {
        public int DialogId { get; set; }
        public int LimitCount { get; set; }
        
        public class GetUsedStickersInDialogQueryHandler : IRequestHandler<GetUsedStickersInDialogQuery, IEnumerable<Sticker>>
        {
            private readonly IUnitOfWork<MessengerContext> _unitOfWork;

            public GetUsedStickersInDialogQueryHandler(IUnitOfWork<MessengerContext> unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<Sticker>> Handle(GetUsedStickersInDialogQuery request, CancellationToken cancellationToken)
            {
                if (request.DialogId > 0)
                {
                    return await _unitOfWork.DbContext.Messages
                        .Where(x => x.UserAccess.DialogId == request.DialogId)
                        .Select(m => m.MessageSticker.Sticker)
                        .ToListAsync(cancellationToken);
                }

                return null;
            }
        }
    }
}