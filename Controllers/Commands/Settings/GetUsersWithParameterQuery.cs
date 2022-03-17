using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Messenger.DB.entity;
using Messenger.Host.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Controllers.Commands.Settings
{
    public class GetUsersWithParameterQuery : IRequest<IEnumerable<User>>
    {
        public string ParamName { get; set; }
        public int LimitCount { get; set; }
        
        public class GetUsersWithParameterQueryHandler : IRequestHandler<GetUsersWithParameterQuery, IEnumerable<User>>
        {
            private readonly IUnitOfWork<MessengerContext> _unitOfWork;

            public GetUsersWithParameterQueryHandler(IUnitOfWork<MessengerContext> unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IEnumerable<User>> Handle(GetUsersWithParameterQuery request, CancellationToken cancellationToken)
            {
                if (string.IsNullOrEmpty(request.ParamName) == false
                    && IsContainsParameter(request.ParamName))
                {
                    return await _unitOfWork.DbContext.Values
                        .Where(x => x.Parameter.Name == request.ParamName)
                        .Select(x => x.IdUserNavigation)
                        .Take(request.LimitCount)
                        .ToListAsync(cancellationToken);
                }
                return null;
            }

            private bool IsContainsParameter(string paramName)
            {
                return _unitOfWork.DbContext.Parameters
                            .Select(x => x.Name)
                            .Contains(paramName);
            }
        }
    }
}