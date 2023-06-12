using System.Net;

namespace FinApp.Core.Exceptions
{
    public class NoAccessException : BaseException
    {
        public NoAccessException() : base("Sorry, you dont access,", HttpStatusCode.BadRequest) { }
    }
}
