using System.Net;

namespace BusinessLogic.Response
{
    public class ResponseFactory<TResponce> where TResponce : BaseResponce
    {
        public Result<TResponce> ConflictResponce(string message, string description)
        {
            return new()
            {
                Error = new ErrorResponce
                {
                    ErrorMessage = message,
                    ErrorDetails = description,
                    Success = false,
                    StatusCode = HttpStatusCode.Conflict
                },
                Responce = null,
                StatusCode = HttpStatusCode.Conflict
            };
        }

        public Result<TResponce> SuccessResponse(TResponce responce)
        {
            return new()
            {
                Error = null,
                Responce = responce,
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}