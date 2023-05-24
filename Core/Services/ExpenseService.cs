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
    public class ExpenseService : IExpenseService
    {
        private readonly ApplicationContext _context;

        public ExpenseService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<CollectionDto<Expense>> GetAll(MoneyFSSearchContext fS)
        {
            PaginationContext paginationContext = new PaginationContext { Page = fS.Page };

            IQueryable<Expense> query = _context.Expenses;
            query = fS.Sort switch
            {
                null => query.OrderBy(x => x.Name),
                _ => query.OrderBy((SortingDirection?)fS.Sort, propertyName: fS.Sort.GetDescription())
            };

            var expense = await query.Where(x => x.UserId == fS.UserId && x.CreatedAt >= fS.MoneyFlow.From && x.CreatedAt <= fS.MoneyFlow.To).Skip(Convert.ToInt32(paginationContext.OffSet)).Take(paginationContext.PageSize).ToListAsync();

            var response = new CollectionDto<Expense>
            {
                Items = expense,
                Total = _context.Expenses.Count()
            };

            return response;
        }

        public async Task<Expense> Get(int id, int userId)
        {
            var expense = await _context.Expenses.SingleOrDefaultAsync(x => x.Id == id);

            if (expense == null)
                throw new ExpenseNotFoudException();
            if (userId != expense.UserId)
                throw new NoAccessException();

            return expense;
        }

        public async Task<int> Create(ExpenseCreateData expenseCreateData, int userId)
        {
            var userExist = _context.Users.SingleOrDefaultAsync(x => x.Id == userId);
            if (userExist != null)
                throw new UserExists();
            if (await _context.Expenses.SingleOrDefaultAsync(x => x.Id == expenseCreateData.CategoryId) == null)
                throw new ExpenseNotFoudException();
            if (_context.Expenses.SingleOrDefaultAsync(x => x.Id == expenseCreateData.CategoryId).Id != userId)
                throw new ExpenseExistException();
            var newExpense = new Expense
            {
                Name = expenseCreateData.Name,
                Summary = expenseCreateData.Summary,
                ExpenseCategoryId = expenseCreateData.CategoryId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UserId = userId
            };

            await _context.Expenses.AddAsync(newExpense);
            await _context.SaveChangesAsync();

            return newExpense.Id;
        }

        public async Task Update(ExpenseUpdateData expenseUpdateData, int userId)
        {
            var expense1 = new Expense();
            var expense = _context.Expenses.SingleOrDefaultAsync(x => x.Id == expense1.Id);
            if (expense == null)
                throw new ExpenseExistException();
            if (userId != expense1.UserId)
                throw new NoAccessException();
            if (await _context.ExpenseCategories.SingleOrDefaultAsync(x => x.Id == expenseUpdateData.CategoryId) == null)
                throw new ExpenseNotFoudException();
            if (_context.ExpenseCategories.SingleOrDefault(x => x.Id == expenseUpdateData.CategoryId).UserId != userId)
                throw new NoAccessException();


            expense1.Name = expenseUpdateData.Name;
            expense1.Summary = expenseUpdateData.Summary;
            expense1.ExpenseCategoryId = expenseUpdateData.CategoryId;
            expense1.UpdatedAt = DateTime.Now;


            _context.Expenses.Update(expense1);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var expenseToDelete = await _context.Expenses.SingleOrDefaultAsync(x => x.Id == id);
            if (expenseToDelete == null)
            {
                throw new ExpenseIsDeletedException();
            }

            _context.Expenses.Remove(expenseToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
