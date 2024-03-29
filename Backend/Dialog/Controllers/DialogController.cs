﻿using System.Threading.Tasks;
using BusinessLogic.Controllers;
using BusinessLogic.Response;
using Dialog.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dialog.Controllers
{
    [Authorize]
    [Route("api/dialogs")]
    public class DialogController : ApiControllerBase
    {
        public DialogController(IMediator mediator) : base(mediator) {}

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddDialogMemberResponse))]
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

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteDialogMemberResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponce))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponce))]
        public async Task<IActionResult> DeleteDialogMember(int userId, int dialogId)
        {
            DeleteDialogMemberCommand request = new DeleteDialogMemberCommand
            {
                DeletedUserId = userId,
                DialogId = dialogId,
                DeletingUserId = HttpContext.User.GetUserId()
            };
            
            return await RequestAsync(request, default);
        }
    }
}