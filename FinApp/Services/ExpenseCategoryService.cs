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
    public class ExpenseCategoryService : IExpenseCategoryService
    {
        private readonly ApplicationContext _context;

        public ExpenseCategoryService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<ResponseType<ExpenseCategory>> GetAll(int userId, int page, CategotiesSort sort)
        {
            var pageResults = 5f;
            var pageCount = Math.Ceiling(_context.expenseCategories.Count() / pageResults);

            IQueryable<ExpenseCategory> query = _context.expenseCategories;
            query = sort switch
            {
                CategotiesSort.NameAsc => query.OrderBy(x => x.Name),
                CategotiesSort.NameDesc => query.OrderByDescending(x => x.Name),
            };

            var expenseCategory = await query.Where(x => x.UserId == userId).Skip((page - 3) * Convert.ToInt32(pageResults)).Take(Convert.ToInt32(pageResults)).ToListAsync();
            
            var response = new ResponseType<ExpenseCategory>
            {
                ListOfType = expenseCategory,
                CurrentPage = page,
                CountPage = Convert.ToInt32(pageCount)
            };

            return response;
        }

        public async Task<ExpenseCategory> Get(int id, int userId)
        {
            var expenseCategory = await _context.expenseCategories.SingleOrDefaultAsync(x => x.Id == id);

            if (expenseCategory == null)
                throw new ExpenseNotFoudException();
            if (userId != expenseCategory.UserId)
                throw new NoAccessException();

            return expenseCategory;
        }

        public async Task<int> Create(ExpenseCreateData expenseCreateData, int userId)
        {
            var userExist = _context.user.SingleOrDefaultAsync(x => x.Id == userId);
            if (userExist != null)
                throw new UserExists();
            if (await _context.expenseCategories.SingleOrDefaultAsync(x => x.Name == expenseCreateData.Name && x.UserId == userId) != null)
                throw new ExpenseExistException();
            var newExpenseCategory = new ExpenseCategory
            {
                Name = expenseCreateData.Name,
                UserId = userId
            };
            await _context.expenseCategories.AddAsync(newExpenseCategory);
            await _context.SaveChangesAsync();

            return newExpenseCategory.Id;
        }

        public async Task Update(ExpenseUpdateData expenseUpdateData, int userId)
        {
            var expenseCategory1 = new ExpenseCategory();
            var expenseCategory = _context.expenseCategories.SingleOrDefaultAsync(x => x.Id == expenseCategory1.Id);
            if (expenseCategory == null)
                throw new ExpenseExistException();
            if (userId != expenseCategory1.UserId)
                throw new NoAccessException();
            expenseCategory1.Name = expenseUpdateData.Name;

            _context.expenseCategories.Update(expenseCategory1);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var expenseCategoryToDelete = await _context.expenseCategories.SingleOrDefaultAsync(x => x.Id == id);
            if (expenseCategoryToDelete == null)
            {
                throw new ExpenseIsDeletedException();
            }

            _context.expenseCategories.Remove(expenseCategoryToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
