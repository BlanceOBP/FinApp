using System.Net;

namespace FinApp.Core.Exceptions
{
    public class IncomeNotFoundException : BaseException
    {
        public IncomeNotFoundException() : base("Your income  are not found.", HttpStatusCode.BadRequest) { }
    }
}
