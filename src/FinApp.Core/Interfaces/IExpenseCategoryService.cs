using FinApp.Core.Models;
using FinApp.Core.SearchContext;
using FinApp.Entities;

namespace FinApp.Core.Interfaces
{
    /// <summary>
    /// Defines methods related to expense category.
    /// </summary>
    public interface IExpenseCategoryService
    {
        /// <summary>
        /// Creates a new expense category.
        /// </summary>
        /// <param name="expenseCreateData">The name of the new expense.</param>
        /// <param name="userId">Current user ID.</param>
        /// <returns>Created expense category.</returns>
        Task<int> Create(ExpenseCreateData expenseCreateData, int userId);

        /// <summary>
        /// Deletes expense category data.
        /// </summary>
        /// <param name="id">ID of expense category to be deleted.</param>
        Task Delete(int id);

        /// <summary>
        /// Get expense category by ID.
        /// </summary>
        /// <param name="id">Desired expense category ID.</param>
        /// <param name="userId">Current user ID.</param>
        /// <returns>Expense category with the specified ID.</returns>
        Task<ExpenseCategory> Get(int id, int userId);

        /// <summary>
        /// Get all expense categories.
        /// </summary>
        /// <param name="page">Users list page.</param>
        /// <param name="sort">Order sorting.</param>
        /// <param name="userId">Current user ID.</param>
        /// <returns>All expense categories of current user.</returns>
        Task<CollectionDto<ExpenseCategory>> GetAll(CategotiesFlowSearchContext flow);

        /// <summary>
        /// Updates expense category data.
        /// </summary>
        /// <param name="expenseUpdateData">Update expense data.</param>
        /// <param name="userId">Current user ID.</param>
        Task Update(ExpenseUpdateData expenseUpdateData, int userId);

    }
}
