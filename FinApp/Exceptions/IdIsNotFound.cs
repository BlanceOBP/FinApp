using FinApp.Exeptions;
using System.Net;

namespace FinApp.Exceptions
{
    public class IdIsNotFound : BaseException
    {
        public IdIsNotFound() : base("Id is not found.", HttpStatusCode.BadRequest) { }
    }
}
