using System.Net;

namespace FinApp.Exeptions
{
    public class BaseException : Exception
    {
        public BaseException(string message, HttpStatusCode code)
        : base(message)
        {
            ErrorCode = code;
        }

        internal static HttpStatusCode ErrorCode;

    }
}
