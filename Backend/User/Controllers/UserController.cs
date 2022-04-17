using System.Threading.Tasks;
using BusinessLogic.Controllers;
using BusinessLogic.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using User.Commands;


namespace User.Controllers
{
    [Authorize]
    [Route("api/user")]
    public class UserController : ApiControllerBase
    {
        public UserController(IMediator mediator) : base(mediator) {}

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChangeUserStatsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponce))]
        public async Task<IActionResult> ChangeUserStats(string username, string password)
        {
            ChangeUserStatsCommand request = new ChangeUserStatsCommand
            {
                UserId = HttpContext.User.GetUserId(),
                Username = username,
                Password = password
            };
            
            return await RequestAsync(request, default);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ChangeUserStatsResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponce))]
        public async Task<IActionResult> GetUserDTO(int userId)
        {

            return null;
        }
    }
}