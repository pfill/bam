using System.Net;

namespace StargateAPI.Shared
{
    public static class ExceptionExtensions
    {
        public static HttpStatusCode GetResponseCode(this Exception ex)
        {
            if (ex is KeyNotFoundException)
            {
                return HttpStatusCode.NotFound;
            }
            else if (ex is ArgumentNullException ||
                     ex is BadHttpRequestException)
            {
                return HttpStatusCode.BadRequest;
            }
            else
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    }
}