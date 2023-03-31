using FinApp.Exeptions;
using System.Net;

namespace FinApp.Exceptions
{
    public class ExpenseExistException : BaseException
    {
        public ExpenseExistException() : base("Expenses exists.", HttpStatusCode.BadRequest) { }
    }
}
