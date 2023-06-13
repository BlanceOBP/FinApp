using System.Net;

namespace FinApp.Core.Exceptions
{
    public class LoginException : BaseException
    {
        public LoginException()
        : base("Login or password entered incorrectly.", HttpStatusCode.BadRequest) { }
    }
}
