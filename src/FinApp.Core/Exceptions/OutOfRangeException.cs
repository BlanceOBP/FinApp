using System.Net;

namespace FinApp.Core.Exceptions
{
    public class OutOfRangeException : BaseException
    {
        public OutOfRangeException() : base("Out of range from query", HttpStatusCode.BadRequest) { }
    }
}
