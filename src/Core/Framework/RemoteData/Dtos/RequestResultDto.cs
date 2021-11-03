using System.Net;

namespace Framework.RemoteData.Dtos
{
    public class RequestResultDto<T>
    {
        public T Output { get; set; }
        public string Message { get; set; }
        public bool IsSucceeded { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
