using System.Net;

namespace FinApp.Core.Exceptions
{
    public class ExpenseExistException : BaseException
    {
        public ExpenseExistException() : base("Expenses exists.", HttpStatusCode.BadRequest) { }
    }
}
