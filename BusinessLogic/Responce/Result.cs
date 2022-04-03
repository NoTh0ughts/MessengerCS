
using System.Net;

namespace BusinessLogic.Response
{
    public record Result<TResponce> where TResponce : BaseResponce
    {
        public TResponce Responce {get;init;}
        public ErrorResponce Error {get; init;}
        public HttpStatusCode StatusCode {get; init;}
    }
}