using FinApp.Exeptions;
using System.Net;

namespace FinApp.Exceptions
{
    public class ExpenseIsDeletedException : BaseException
    {
        public ExpenseIsDeletedException() : base("Expense is deleted.", HttpStatusCode.BadRequest) { }
    }
}
