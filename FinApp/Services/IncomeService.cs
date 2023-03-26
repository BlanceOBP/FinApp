using FinApp.DataBase;
using FinApp.Entity;
using FinApp.Exceptions;
using FinApp.Interfaces;
using FinApp.MiddleEntity;
using Microsoft.EntityFrameworkCore;

namespace FinApp.Services
{
    public class IncomeService : IIncomeService
    {
        private readonly ApplicationContext acb;

        public IncomeService(ApplicationContext _acb)
        {
            acb = _acb;
        }

        public async Task<List<Income>> GetAll(int userId, MoneyFlow moneyFlow)
        {
            var income = await acb.income.Where(x => x.User == userId && x.DateOfCreate >= moneyFlow.From && x.DateOfCreate <= moneyFlow.To).ToListAsync();
            return income;
        }

        public async Task<Income> Get(int id, int userId)
        {
            var income = await acb.income.SingleOrDefaultAsync(x => x.Id == id);

            if (income == null)
                throw new IncomeNotFoundException();
            if (userId != income.User)
                throw new NoAccessException();

            return income;
        }

        public async Task<int> Create(IncomeCreateData incomeCreateData, int userId)
        {
            var userExist = acb.user.SingleOrDefaultAsync(x => x.Id == userId);
            if (userExist != null)
                throw new UserExists();
            if (await acb.sourcesOfIncome.SingleOrDefaultAsync(x => x.Id == incomeCreateData.CategoryId) == null)
                throw new IncomeNotFoundException();
            if (acb.sourcesOfIncome.SingleOrDefaultAsync(x => x.Id == incomeCreateData.CategoryId).Id != userId)
                throw new IncomeSourceExistException();
            var newIncome = new Income
            {
                Name = incomeCreateData.Name,
                Summary = incomeCreateData.Summary,
                SourceOfIncomeId = incomeCreateData.CategoryId,
                DateOfCreate = DateTime.Now,
                DateOfEdit = DateTime.Now,
                User = userId
            };
            await acb.income.AddAsync(newIncome);
            await acb.SaveChangesAsync();

            return newIncome.Id;
        }

        public async Task Update(IncomeUpdateData incomeUpdateData, int userId)
        {
            var income1 = new Income();
            var income = acb.income.SingleOrDefaultAsync(x => x.Id == income1.Id);
            if (income == null)
                throw new IncomeSourceExistException();
            if (userId != income1.User)
                throw new NoAccessException();
            if (await acb.sourcesOfIncome.SingleOrDefaultAsync(x => x.Id == incomeUpdateData.CategoryId) == null)
                throw new IncomeNotFoundException();
            if (acb.sourcesOfIncome.SingleOrDefault(x => x.Id == incomeUpdateData.CategoryId).UserId != userId)
                throw new NoAccessException();


            income1.Name = incomeUpdateData.Name;
            income1.Summary = incomeUpdateData.Summary;
            income1.SourceOfIncomeId = incomeUpdateData.CategoryId;
            income1.DateOfEdit = DateTime.Now;


            acb.income.Update(income1);
            await acb.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var incomeToDelete = await acb.income.SingleOrDefaultAsync(x => x.Id == id);
            if (incomeToDelete == null)
            {
                throw new IncomeIsDeletedException();
            }
            acb.income.Remove(incomeToDelete);
            await acb.SaveChangesAsync();
        }
    }
}
