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
        private readonly ApplicationContext _context;

        public IncomeService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<List<Income>> GetAll(int userId, MoneyFlow moneyFlow)
        {
            var income = await _context.income.Where(x => x.UserId == userId && x.CreatedAt >= moneyFlow.From && x.CreatedAt <= moneyFlow.To).ToListAsync();

            return income;
        }

        public async Task<Income> Get(int id, int userId)
        {
            var income = await _context.income.SingleOrDefaultAsync(x => x.Id == id);

            if (income == null)
                throw new IncomeNotFoundException();
            if (userId != income.UserId)
                throw new NoAccessException();

            return income;
        }

        public async Task<int> Create(IncomeCreateData incomeCreateData, int userId)
        {
            var userExist = _context.user.SingleOrDefaultAsync(x => x.Id == userId);
            if (userExist != null)
                throw new UserExists();
            if (await _context.sourcesOfIncome.SingleOrDefaultAsync(x => x.Id == incomeCreateData.CategoryId) == null)
                throw new IncomeNotFoundException();
            if (_context.sourcesOfIncome.SingleOrDefaultAsync(x => x.Id == incomeCreateData.CategoryId).Id != userId)
                throw new IncomeSourceExistException();
            var newIncome = new Income
            {
                Name = incomeCreateData.Name,
                Summary = incomeCreateData.Summary,
                SourceOfIncomeId = incomeCreateData.CategoryId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UserId = userId
            };

            await _context.income.AddAsync(newIncome);
            await _context.SaveChangesAsync();

            return newIncome.Id;
        }

        public async Task Update(IncomeUpdateData incomeUpdateData, int userId)
        {
            var income1 = new Income();
            var income = _context.income.SingleOrDefaultAsync(x => x.Id == income1.Id);
            if (income == null)
                throw new IncomeSourceExistException();
            if (userId != income1.UserId)
                throw new NoAccessException();
            if (await _context.sourcesOfIncome.SingleOrDefaultAsync(x => x.Id == incomeUpdateData.CategoryId) == null)
                throw new IncomeNotFoundException();
            if (_context.sourcesOfIncome.SingleOrDefault(x => x.Id == incomeUpdateData.CategoryId).UserId != userId)
                throw new NoAccessException();


            income1.Name = incomeUpdateData.Name;
            income1.Summary = incomeUpdateData.Summary;
            income1.SourceOfIncomeId = incomeUpdateData.CategoryId;
            income1.UpdatedAt = DateTime.Now;


            _context.income.Update(income1);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var incomeToDelete = await _context.income.SingleOrDefaultAsync(x => x.Id == id);
            if (incomeToDelete == null)
            {
                throw new IncomeIsDeletedException();
            }

            _context.income.Remove(incomeToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
