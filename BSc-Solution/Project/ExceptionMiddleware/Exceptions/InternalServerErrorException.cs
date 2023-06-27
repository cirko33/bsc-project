using System.Net;

namespace Project.ExceptionMiddleware.Exceptions
{
    public class InternalServerErrorException : BaseException
    {
        public InternalServerErrorException(string message, List<string>? messages = null, HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError) : base(message, messages, httpStatusCode)
        {
        }
    }
}
