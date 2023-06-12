using System.Net;

namespace FinApp.Core.Exceptions
{
    public class IncomeSourceNotFound : BaseException
    {
        public IncomeSourceNotFound() : base("Your income source are not found.", HttpStatusCode.BadRequest) { }
    }
}
