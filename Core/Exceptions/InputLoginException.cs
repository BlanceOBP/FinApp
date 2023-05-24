using FinApp.Exeptions;
using System.Net;

namespace FinApp.Exceptions
{
    public class InputLoginException : BaseException
    {
      public InputLoginException()  : base("Email or login already in use.", HttpStatusCode.BadRequest) { }
    }
}
