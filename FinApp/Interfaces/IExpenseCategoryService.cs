using FinApp.Entity;
using FinApp.MiddleEntity;

namespace FinApp.Interfaces
{
    public interface IExpenseCategoryService
    {
        Task<int> Create(ExpenseCreateData expenseCreateData, int userId);
        Task Delete(int id);
        Task<ExpenseCategory> Get(int id, int userId);
        Task<List<ExpenseCategory>> GetAll(int userId);
        Task Update(ExpenseUpdateData expenseUpdateData, int userId);
    }
}
