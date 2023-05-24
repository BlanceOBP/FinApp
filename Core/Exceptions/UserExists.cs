using FinApp.Exeptions;
using System.Net;

namespace FinApp.Exceptions
{
    public class UserExists : BaseException
    {
        public UserExists() : base("User exists.", HttpStatusCode.BadRequest) { }
    }
}
