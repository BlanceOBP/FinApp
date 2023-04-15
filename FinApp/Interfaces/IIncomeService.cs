using FinApp.Entity;
using FinApp.EnumValue;
using FinApp.MiddleEntity;

namespace FinApp.Interfaces
{
    /// <summary>
    /// Defines methods related to income.
    /// </summary>
    public interface IIncomeService
    {
        /// <summary>
        /// Creates a new income.
        /// </summary>
        /// <param name="userId">Current user ID.</param>
        /// <param name="incomeCreateData">Desired income data.</param>
        /// <returns>Created income.</returns>
        Task<int> Create(IncomeCreateData incomeCreateData, int userId);

        /// <summary>
        /// Deletes income category.
        /// </summary>
        /// <param name="id">Desired income ID.</param>
        Task Delete(int id);

        /// <summary>
        /// Get income by ID.
        /// </summary>
        /// <param name="id">ID of the desired income.</param>
        /// <param name="userId">Current user ID.</param>
        /// <returns>Get income.</returns>
        Task<Income> Get(int id, int userId);

        /// <summary>
        /// Get all income for the specified period.
        /// </summary>
        /// <param name="page">Users list page.</param>
        /// <param name="sort">Order sorting.</param>
        /// <param name="userId">Current user ID.</param>
        /// <param name="moneyFlow">Specified period of time.</param>
        /// <returns>Get all income.</returns>
        Task<ResponseType<Income>> GetAll(int userId, MoneyFlow moneyFlow, int page, MoneyFlowSort sort);

        /// <summary>
        /// Updates income data.
        /// </summary>
        /// <param name="userId">Current user ID.</param>
        /// <param name="incomeUpdateData">Income data to update.</param>
        Task Update(IncomeUpdateData incomeUpdateData, int userId);

    }
}
