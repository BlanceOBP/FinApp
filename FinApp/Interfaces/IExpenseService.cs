using FinApp.Entity;
using FinApp.MiddleEntity;

namespace FinApp.Interfaces
{
    public interface IExpenseService
    {
        Task<int> Create(ExpenseCreateData expenseCreateData, int userId);
        Task Delete(int id);
        Task<Expense> Get(int id, int userId);
        Task<List<Expense>> GetAll(int userId, MoneyFlow moneyFlow);
        Task Update(ExpenseUpdateData expenseUpdateData, int userId);
    }
}
