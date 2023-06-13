using System.Net;

namespace FinApp.Core.Exceptions
{
    public class UserExists : BaseException
    {
        public UserExists() : base("User exists.", HttpStatusCode.BadRequest) { }
    }
}
