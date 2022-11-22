using FinApp.Exeptions;
using System.Net;

namespace FinApp.Exceptions
{
    public class LoginException : BaseException
    {
        public LoginException()
        : base("Login or password entered incorrectly.", HttpStatusCode.BadRequest) { }
    }
}
