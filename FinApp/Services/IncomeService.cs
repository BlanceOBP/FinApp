using FinApp.DataBase;
using FinApp.Entity;
using FinApp.EnumValue;
using FinApp.Exceptions;
using FinApp.Interfaces;
using FinApp.MiddleEntity;
using FinApp.SearchContext;
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

        public async Task<CollectionDto<Incomes>> GetAll(MoneyFSSearchContext fS)
        {
            PaginationContext paginationContext = new PaginationContext { Page = fS.Page };

            IQueryable<Incomes> query = _context.Income;
            query = fS.Sort switch
            {
                null => query.OrderBy(x => x.Name),
                _ => query.OrderBy((SortingDirection?)fS.Sort, propertyName: fS.Sort.GetDescription())
            };

            var income = await query.Where(x => x.UserId == fS.UserId && x.CreatedAt >= fS.MoneyFlow.From && x.CreatedAt <= fS.MoneyFlow.To).Skip(Convert.ToInt32(paginationContext.OffSet)).Take(paginationContext.PageSize).ToListAsync();

            var response = new CollectionDto<Incomes>
            {
                Items = income,
                Total = _context.Income.Count()
            };

            return response;
        }

        public async Task<Incomes> Get(int id, int userId)
        {
            var income = await _context.Income.SingleOrDefaultAsync(x => x.Id == id);

            if (income == null)
                throw new IncomeNotFoundException();
            if (userId != income.UserId)
                throw new NoAccessException();

            return income;
        }

        public async Task<int> Create(IncomeCreateData incomeCreateData, int userId)
        {
            var userExist = _context.User.SingleOrDefaultAsync(x => x.Id == userId);
            if (userExist != null)
                throw new UserExists();
            if (await _context.SourcesOfIncome.SingleOrDefaultAsync(x => x.Id == incomeCreateData.CategoryId) == null)
                throw new IncomeNotFoundException();
            if (_context.SourcesOfIncome.SingleOrDefaultAsync(x => x.Id == incomeCreateData.CategoryId).Id != userId)
                throw new IncomeSourceExistException();
            var newIncome = new Incomes
            {
                Name = incomeCreateData.Name,
                Summary = incomeCreateData.Summary,
                SourceOfIncomeId = incomeCreateData.CategoryId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UserId = userId
            };

            await _context.Income.AddAsync(newIncome);
            await _context.SaveChangesAsync();

            return newIncome.Id;
        }

        public async Task Update(IncomeUpdateData incomeUpdateData, int userId)
        {
            var income1 = new Incomes();
            var income = _context.Income.SingleOrDefaultAsync(x => x.Id == income1.Id);
            if (income == null)
                throw new IncomeSourceExistException();
            if (userId != income1.UserId)
                throw new NoAccessException();
            if (await _context.SourcesOfIncome.SingleOrDefaultAsync(x => x.Id == incomeUpdateData.CategoryId) == null)
                throw new IncomeNotFoundException();
            if (_context.SourcesOfIncome.SingleOrDefault(x => x.Id == incomeUpdateData.CategoryId).UserId != userId)
                throw new NoAccessException();


            income1.Name = incomeUpdateData.Name;
            income1.Summary = incomeUpdateData.Summary;
            income1.SourceOfIncomeId = incomeUpdateData.CategoryId;
            income1.UpdatedAt = DateTime.Now;


            _context.Income.Update(income1);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var incomeToDelete = await _context.Income.SingleOrDefaultAsync(x => x.Id == id);
            if (incomeToDelete == null)
            {
                throw new IncomeIsDeletedException();
            }

            _context.Income.Remove(incomeToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
