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
    public class GetUsersInDialogQuery: IRequest<IEnumerable<User>>
    {
        public int DialogId { get; set; }
        
        public class GetUsersInDialogQueryHandler : IRequestHandler<GetUsersInDialogQuery, IEnumerable<User>>
        {
            private readonly IUnitOfWork<MessengerContext> _unitOfWork;

            public GetUsersInDialogQueryHandler(IUnitOfWork<MessengerContext> unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }


            public async Task<IEnumerable<User>> Handle(GetUsersInDialogQuery request, CancellationToken cancellationToken = default)
            {
                if (request.DialogId > 0)
                {
                    return await _unitOfWork.DbContext.UserAccesses
                        .Where(ud => ud.DialogId == request.DialogId)
                        .Select(ud => ud.User)
                        .ToListAsync(cancellationToken);
                }

                return null;
            }
        }
    }
}