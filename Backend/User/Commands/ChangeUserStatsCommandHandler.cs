using System.Threading;
using System.Threading.Tasks;
using BusinessLogic.Response;
using Data.Entity;
using Data.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace User.Commands
{
    public class ChangeUserStatsCommandHandler : 
        IRequestHandler<ChangeUserStatsCommand, Result<ChangeUserStatsResponse>>
    {
        private IUnitOfWork<MessengerContext> unitOfWork;
        private ResponseFactory<ChangeUserStatsResponse> _responseFactory;
        
        public ChangeUserStatsCommandHandler
            (IUnitOfWork<MessengerContext> unitOfWork, ResponseFactory<ChangeUserStatsResponse> responseFactory)
        {
            this.unitOfWork = unitOfWork;
            _responseFactory = responseFactory;
        }
        
        public async Task<Result<ChangeUserStatsResponse>> Handle
            (ChangeUserStatsCommand request, CancellationToken cancellationToken)
        {
            // TODO переделать после добавления USERNAME
            var user = await unitOfWork.DbContext.Users.ToListAsync();
            
            return null;
        }
    }
}