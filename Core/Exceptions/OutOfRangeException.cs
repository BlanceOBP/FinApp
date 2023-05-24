using FinApp.Exeptions;
using System.Net;

namespace FinApp.Exceptions
{
    public class OutOfRangeException : BaseException
    {
        public OutOfRangeException() : base("Out of range from query", HttpStatusCode.BadRequest) { }
    }
}
