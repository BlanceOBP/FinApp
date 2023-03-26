using FinApp.Exeptions;
using System.Net;

namespace FinApp.Exceptions
{
    public class IncomeIsDeletedException : BaseException
    {
        public IncomeIsDeletedException() : base("Income is deleted.", HttpStatusCode.BadRequest) { }
    }
}
