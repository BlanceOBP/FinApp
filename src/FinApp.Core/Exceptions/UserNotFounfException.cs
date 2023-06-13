using System.Net;

namespace FinApp.Core.Exceptions
{
    public class UserNotFounfException : BaseException
    {
        public UserNotFounfException() : base("User is not found.", HttpStatusCode.BadRequest) { }
    }
}
