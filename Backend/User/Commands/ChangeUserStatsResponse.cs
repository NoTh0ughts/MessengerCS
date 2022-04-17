using BusinessLogic.Constants;
using BusinessLogic.Response;
using Data.Entity;

namespace User.Commands
{
    public record ChangeUserStatsResponse: BaseResponce<ChangeUserStatsResponse>
    {
        public UserDTO NewStats { get; set; }

        public static ChangeUserStatsResponse FromSuccess(UserDTO user) => new()
        {
            Success = true,
            NewStats = user,
            Message = ResponceMessageCodes.Success
        };
    }
}