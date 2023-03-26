using FinApp.Entity;
using FinApp.MiddleEntity;

namespace FinApp.Interfaces
{
    public interface IIncomeService
    {
        Task<int> Create(IncomeCreateData incomeCreateData, int userId);
        Task Delete(int id);
        Task<Income> Get(int id, int userId);
        Task<List<Income>> GetAll(int userId, MoneyFlow moneyFlow);
        Task Update(IncomeUpdateData incomeUpdateData, int userId);
    }
}
