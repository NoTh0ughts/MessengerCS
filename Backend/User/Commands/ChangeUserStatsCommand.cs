using BusinessLogic.Response;
using MediatR;

namespace User.Commands
{
    public class ChangeUserStatsCommand : IRequest<Result<ChangeUserStatsResponse>>
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}