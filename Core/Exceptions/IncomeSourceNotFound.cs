using FinApp.Exeptions;
using System.Net;

namespace FinApp.Exceptions
{
    public class IncomeSourceNotFound : BaseException
    {
        public IncomeSourceNotFound() : base("Your income source are not found.", HttpStatusCode.BadRequest) { }
    }
}
