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
    public class GetUsersInDialogQuery: IRequest<IEnumerable<string>>
    {
        public int DialogId { get; set; }
        
        public class GetUsersInDialogQueryHandler : IRequestHandler<GetUsersInDialogQuery, IEnumerable<string>>
        {
            private readonly IUnitOfWork<MessengerContext> _unitOfWork;

            public GetUsersInDialogQueryHandler(IUnitOfWork<MessengerContext> unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }


            public async Task<IEnumerable<string>> Handle(GetUsersInDialogQuery request, CancellationToken cancellationToken = default)
            {
                if (request.DialogId > 0)
                {
                    return await (from ua in _unitOfWork.DbContext.UserAccesses
                        join u in _unitOfWork.DbContext.Users on ua.UserId equals u.Id
                        where ua.DialogId == request.DialogId
                        select u.Name).ToListAsync();
                }

                return null;
            }
        }
    }
}