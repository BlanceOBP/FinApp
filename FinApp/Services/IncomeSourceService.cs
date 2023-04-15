using FinApp.Controllers;
using FinApp.DataBase;
using FinApp.Entity;
using FinApp.EnumValue;
using FinApp.Exceptions;
using FinApp.Interfaces;
using FinApp.MiddleEntity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinApp.Services
{
    public class IncomeSourceService : IIncomeSourceService
    {
        private readonly ApplicationContext _context;

        public IncomeSourceService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ResponseType<SourceOfIncome>> GetAll(int userId, int page, CategotiesSort sort)
        {
            var pageResults = 5f;
            var pageCount = Math.Ceiling(_context.sourcesOfIncome.Count() / pageResults);

            IQueryable<SourceOfIncome> query = _context.sourcesOfIncome;
            query = sort switch
            {
                CategotiesSort.NameAsc => query.OrderBy(x => x.Name),
                CategotiesSort.NameDesc => query.OrderByDescending(x => x.Name),
            };

            var incomeSources = await query.Where(x => x.UserId == userId).Skip((page - 3) * Convert.ToInt32(pageResults)).Take(Convert.ToInt32(pageResults)).ToListAsync();

            var response = new ResponseType<SourceOfIncome>
            {
                ListOfType = incomeSources,
                CurrentPage = page,
                CountPage = Convert.ToInt32(pageCount)
            };

            return response;
        }

        public async Task<SourceOfIncome> Get(int id, int userId)
        {
            var incomeSource = await _context.sourcesOfIncome.SingleOrDefaultAsync(x => x.Id == id);

            if (incomeSource == null)
                throw new IncomeSourceNotFound();
            if (userId != incomeSource.UserId)
                throw new NoAccessException();
            
            return incomeSource;
        }

        public async Task<int> Create(IncomeCreateData incomeCreateData, int userId)
        {
            var userExist = _context.user.SingleOrDefaultAsync(x => x.Id == userId);
            if (userExist != null)
                throw new UserExists();
            if (await _context.sourcesOfIncome.SingleOrDefaultAsync(x => x.Name == incomeCreateData.Name && x.UserId == userId) != null)
                throw new IncomeSourceExistException();
            var newIncomeSource = new SourceOfIncome
            {
                Name = incomeCreateData.Name,
                UserId = userId
            };

            await _context.sourcesOfIncome.AddAsync(newIncomeSource);
            await _context.SaveChangesAsync();

            return newIncomeSource.Id;
        }

        public async Task Update(IncomeUpdateData incomeUpdateData, int userId)
        {
            var incomeSource1 = new SourceOfIncome();
            var incomeSource = _context.sourcesOfIncome.SingleOrDefaultAsync(x => x.Id == incomeSource1.Id);
            if (incomeSource == null)
                throw new IncomeSourceExistException();
            if (userId != incomeSource1.UserId)
                throw new NoAccessException();
            incomeSource1.Name = incomeUpdateData.Name;

            _context.sourcesOfIncome.Update(incomeSource1);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var incomeSourceToDelete = await _context.sourcesOfIncome.SingleOrDefaultAsync(x => x.Id == id);
            if (incomeSourceToDelete == null)
            {
                throw new IncomeIsDeletedException();
            }

            _context.sourcesOfIncome.Remove(incomeSourceToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
