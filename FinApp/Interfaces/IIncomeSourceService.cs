using FinApp.Entity;
using FinApp.MiddleEntity;

namespace FinApp.Interfaces
{
    public interface IIncomeSourceService
    {
        Task<int> Create(IncomeCreateData incomeCreateData, int userId);
        Task Delete(int id);
        Task<SourceOfIncome> Get(int id, int userId);
        Task<List<SourceOfIncome>> GetAll(int userId);
        Task Update(IncomeUpdateData incomeUpdateData, int userId);
    }
}
