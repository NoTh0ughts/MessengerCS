using BusinessLogic.Response;
using MediatR;

namespace Dialog.Commands
{
    public class AddDialogMemberCommand : IRequest<Result<AddDialogMemberResponce>>
    {
        public int InvitedUserId { get; set; }
        public int DialogId { get; set; }
        public int InvitingUserId { get; set; }
    }
}