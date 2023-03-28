using FinApp.Entity;
using FinApp.MiddleEntity;

namespace FinApp.Interfaces
{
    /// <summary>
    /// Defines methods related to expenses.
    /// </summary>
    public interface IExpenseService
    {
        /// <summary>
        /// Creates a new expense.
        /// </summary>
        /// <param name="userId">Current user ID.</param>
        /// <param name="expenseCreateData">Desired expense data.</param>
        /// <returns>Created expense.</returns>
        Task<int> Create(ExpenseCreateData expenseCreateData, int userId);

        /// <summary>
        /// Deletes expense data.
        /// </summary>
        /// <param name="id">ID of income to be deleted.</param>
        Task Delete(int id);

        /// <summary>
        /// Get expense by ID.
        /// </summary>
        /// <param name="id">ID of the desired expense.</param>
        /// <param name="userId">Current user ID.</param>
        /// <returns>Expense with the specified ID.</returns>
        Task<Expense> Get(int id, int userId);

        /// <summary>
        /// Get all expenses for the specified period.
        /// </summary>
        /// <param name="userId">Current user ID.</param>
        /// <param name="moneyFlow">Specified period of time.</param>
        /// <returns>List of expenses.</returns>
        Task<List<Expense>> GetAll(int userId, MoneyFlow moneyFlow);

        /// <summary>
        /// Updates expense data.
        /// </summary>
        /// <param name="userId">Current user ID.</param>
        /// <param name="expenseUpdateData">Expense data to update.</param>
        Task Update(ExpenseUpdateData expenseUpdateData, int userId);

    }
}
