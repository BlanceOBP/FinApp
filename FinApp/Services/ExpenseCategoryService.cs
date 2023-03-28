using FinApp.DataBase;
using FinApp.Entity;
using FinApp.Exceptions;
using FinApp.Interfaces;
using FinApp.MiddleEntity;
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

        public async Task<List<ExpenseCategory>> GetAll(int userId)
        {
            var expenseCategory = await _context.expenseCategories.Where(x => x.UserId == userId).ToListAsync();

            return expenseCategory;
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
