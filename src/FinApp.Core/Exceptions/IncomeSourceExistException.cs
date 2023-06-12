using System.Net;

namespace FinApp.Core.Exceptions
{
    public class IncomeSourceExistException : BaseException
    {
        public IncomeSourceExistException() : base("Income source exists.", HttpStatusCode.BadRequest) { }
    }
}
