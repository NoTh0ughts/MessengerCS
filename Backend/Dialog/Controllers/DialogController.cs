using System.Threading.Tasks;
using BusinessLogic.Controllers;
using BusinessLogic.Response;
using Dialog.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dialog.Controllers
{
    public class DialogController : ApiControllerBase
    {
        public DialogController(IMediator mediator) : base(mediator) {}

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddDialogMemberResponce))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponce))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponce))]
        public async Task<IActionResult> AddDialogMember(int userId, int dialogId)
        {
            AddDialogMemberCommand request = new AddDialogMemberCommand
            {
                InvitedUserId = userId,
                DialogId = dialogId,
                InvitingUserId = HttpContext.User.GetUserId()
            };

            return await RequestAsync(request, default);
        }
    }
}