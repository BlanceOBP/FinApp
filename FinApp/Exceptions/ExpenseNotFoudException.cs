using FinApp.Exeptions;
using System.Net;

namespace FinApp.Exceptions
{
    public class ExpenseNotFoudException : BaseException
    {
        public ExpenseNotFoudException() : base("Expense is not found.", HttpStatusCode.BadRequest) { }
    }
}
