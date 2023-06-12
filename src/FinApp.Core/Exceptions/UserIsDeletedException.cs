using System.Net;

namespace FinApp.Core.Exceptions
{
    public class UserIsDeletedException : BaseException
    {
        public UserIsDeletedException() : base("User is deleted.", HttpStatusCode.BadRequest) { }
    }
}
