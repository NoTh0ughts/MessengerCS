using System.Net;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using BusinessLogic.Response;

namespace BusinessLogic.Controllers
{
    public class ApiControllerBase : ControllerBase
    {
        protected readonly IMediator mediator;
        
        public ApiControllerBase(IMediator mediator)
        {
            this.mediator = mediator;
        }

        protected async Task<IActionResult> RequestAsync<TResponce>(IRequest<Result<TResponce>> request,
                        CancellationToken cancellationToken) where TResponce : BaseResponce
        {
            var responce = await mediator.Send(request, cancellationToken);

            return responce.StatusCode switch
            {
                HttpStatusCode.NotFound =>      NotFound(responce.Error),
                HttpStatusCode.Unauthorized =>  Unauthorized(responce.Error),
                HttpStatusCode.BadRequest =>    BadRequest(responce.Error),
                HttpStatusCode.Conflict =>      Conflict(responce.Error),

                _ => Ok(responce.Responce)
            };
        }
    }
}