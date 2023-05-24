using FinApp.Exeptions;
using System.Net;

namespace FinApp.Exceptions
{
    public class IncomeNotFoundException : BaseException
    {
        public IncomeNotFoundException() : base("Your income  are not found.", HttpStatusCode.BadRequest) { }
    }
}
