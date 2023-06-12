using System.Net;

namespace FinApp.Core.Exceptions
{
    public class IncomeIsDeletedException : BaseException
    {
        public IncomeIsDeletedException() : base("Income is deleted.", HttpStatusCode.BadRequest) { }
    }
}
