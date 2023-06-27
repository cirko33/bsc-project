using System.Net;

namespace Project.ExceptionMiddleware.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message, List<string>? messages = null, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest) : base(message, messages, httpStatusCode)
        {
        }
    }
}
