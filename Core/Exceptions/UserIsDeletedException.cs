using FinApp.Exeptions;
using System.Net;

namespace FinApp.Exceptions
{
    public class UserIsDeletedException : BaseException
    {
        public UserIsDeletedException() : base("User is deleted.", HttpStatusCode.BadRequest) { }
    }
}
