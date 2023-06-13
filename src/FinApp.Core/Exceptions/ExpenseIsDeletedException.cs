using System.Net;

namespace FinApp.Core.Exceptions
{
    public class ExpenseIsDeletedException : BaseException
    {
        public ExpenseIsDeletedException() : base("Expense is deleted.", HttpStatusCode.BadRequest) { }
    }
}
