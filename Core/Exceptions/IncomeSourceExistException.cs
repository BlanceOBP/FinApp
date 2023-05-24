using FinApp.Exeptions;
using System.Net;

namespace FinApp.Exceptions
{
    public class IncomeSourceExistException : BaseException
    {
        public IncomeSourceExistException() : base("Income source exists.", HttpStatusCode.BadRequest) { }
    }
}
