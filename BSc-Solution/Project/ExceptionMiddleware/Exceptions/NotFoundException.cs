using System.Net;

namespace Project.ExceptionMiddleware.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message, List<string>? messages = null, HttpStatusCode httpStatusCode = HttpStatusCode.NotFound) : base(message, messages, httpStatusCode)
        {
        }
    }
}
