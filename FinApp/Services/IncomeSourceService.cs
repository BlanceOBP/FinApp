using FinApp.Controllers;
using FinApp.DataBase;
using FinApp.Entity;
using FinApp.Exceptions;
using FinApp.Interfaces;
using FinApp.MiddleEntity;
using Microsoft.EntityFrameworkCore;

namespace FinApp.Services
{
    public class IncomeSourceService : IIncomeSourceService
    {
        private readonly ApplicationContext acb;

        public IncomeSourceService(ApplicationContext _acb)
        {
            acb = _acb;
        }

        public async Task<List<SourceOfIncome>> GetAll(int userId)
        {
            var incomeSources = await acb.sourcesOfIncome.Where(x => x.UserId == userId).ToListAsync();
            return incomeSources;
        }

        public async Task<SourceOfIncome> Get(int id, int userId)
        {
            var incomeSource = await acb.sourcesOfIncome.SingleOrDefaultAsync(x => x.Id == id);

            if (incomeSource == null)
                throw new IncomeSourceNotFound();
            if (userId != incomeSource.UserId)
                throw new NoAccessException();
            
            return incomeSource;
        }

        public async Task<int> Create(IncomeCreateData incomeCreateData, int userId)
        {
            var userExist = acb.user.SingleOrDefaultAsync(x => x.Id == userId);
            if (userExist != null)
                throw new UserExists();
            if (await acb.sourcesOfIncome.SingleOrDefaultAsync(x => x.Name == incomeCreateData.Name && x.UserId == userId) != null)
                throw new IncomeSourceExistException();
            var newIncomeSource = new SourceOfIncome
            {
                Name = incomeCreateData.Name,
                UserId = userId
            };
            await acb.sourcesOfIncome.AddAsync(newIncomeSource);
            await acb.SaveChangesAsync();

            return newIncomeSource.Id;
        }

        public async Task Update(IncomeUpdateData incomeUpdateData, int userId)
        {
            var incomeSource1 = new SourceOfIncome();
            var incomeSource = acb.sourcesOfIncome.SingleOrDefaultAsync(x => x.Id == incomeSource1.Id);
            if (incomeSource == null)
                throw new IncomeSourceExistException();
            if (userId != incomeSource1.UserId)
                throw new NoAccessException();
            incomeSource1.Name = incomeUpdateData.Name;
            acb.sourcesOfIncome.Update(incomeSource1);
            await acb.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var incomeSourceToDelete = await acb.user.SingleOrDefaultAsync(x => x.Id == id);
            if (incomeSourceToDelete == null)
            {
                throw new UserIsDeletedException();
            }
            acb.user.Remove(incomeSourceToDelete);
            await acb.SaveChangesAsync();
        }
    }
}
