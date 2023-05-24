using FinApp.Entity;
using FinApp.MiddleEntity;
using FinApp.SearchContext;

namespace FinApp.Interfaces
{
    /// <summary>
    /// Defines methods related to source of income.
    /// </summary>
    public interface IIncomeSourceService
    {
        /// <summary>
        /// Creates a new income source category.
        /// </summary>
        /// <param name="incomeCreateData">The name of the new income.</param>
        /// <param name="userId">Current user ID.</param>
        /// <returns>Created income source.</returns>
        Task<int> Create(IncomeCreateData incomeCreateData, int userId);

        /// <summary>
        /// Deletes income source category.
        /// </summary>
        /// <param name="id">Desired income source ID.</param>
        Task Delete(int id);

        /// <summary>
        /// Get income source category by ID.
        /// </summary>
        /// <param name="id">Desired income source ID.</param>
        /// <param name="userId">Current user ID.</param>
        /// <returns>Get income source category by ID.</returns>
        Task<SourceOfIncome> Get(int id, int userId);

        /// <summary>
        /// Get all income source categories.
        /// </summary>
        /// <param name="sort">Order sorting, current user ID and users list page .</param>
        /// <returns>Get all income source category.</returns>
        Task<CollectionDto<SourceOfIncome>> GetAll(CategotiesFlowSearchContext sort);

        /// <summary>
        /// Updates income source category data.
        /// </summary>
        /// <param name="incomeUpdateData">Income update data.</param>
        /// <param name="userId">Current user ID.</param>
        Task Update(IncomeUpdateData incomeUpdateData, int userId);

    }
}
