using BusinessLogic.Response;
using MediatR;

namespace Dialog.Commands
{
    public class DeleteDialogMemberCommand : IRequest<Result<DeleteDialogMemberResponse>>
    {
        public int DialogId { get; set; }
        public int DeletedUserId { get; set; }
        public int DeletingUserId { get; set; }
    }
}