using System.Net;

namespace FinApp.Core.Exceptions
{
    public class ExpenseNotFoudException : BaseException
    {
        public ExpenseNotFoudException() : base("Expense is not found.", HttpStatusCode.BadRequest) { }
    }
}
