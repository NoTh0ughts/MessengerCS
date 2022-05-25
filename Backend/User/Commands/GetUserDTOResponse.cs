using BusinessLogic.Response;
using Data.Entity;

namespace User.Commands
{
    public record GetUserDTOResponse : BaseResponce<GetUserDTOResponse>
    {
        public UserDTO OtherUser { get; set; }
        public static GetUserDTOResponse FromSuccess(UserDTO otherUser) => new()
        {
            Success = true,
            OtherUser = otherUser
        };

    }
}