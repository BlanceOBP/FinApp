using FinApp.Exeptions;
using System.Net;

namespace FinApp.Exceptions
{
    public class UserNotFounfException : BaseException
    {
        public UserNotFounfException() : base("User is not found.", HttpStatusCode.BadRequest) { }
    }
}
