using System.Net;

namespace FinApp.Core.Exceptions
{
    public class IdIsNotFound : BaseException
    {
        public IdIsNotFound() : base("Id is not found.", HttpStatusCode.BadRequest) { }
    }
}
