using FinApp.Exeptions;
using System.Net;

namespace FinApp.Exceptions
{
    public class NoAccessException : BaseException
    {
        public NoAccessException() : base("Sorry, you dont access,", HttpStatusCode.BadRequest) { }
    }
}
