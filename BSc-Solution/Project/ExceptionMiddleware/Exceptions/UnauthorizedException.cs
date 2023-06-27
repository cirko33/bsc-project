using System.Net;

namespace Project.ExceptionMiddleware.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string message, List<string>? messages = null, HttpStatusCode httpStatusCode = HttpStatusCode.Unauthorized) : base(message, messages, httpStatusCode)
        {
        }
    }
}
