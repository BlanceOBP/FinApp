using FinApp.DataBase;
using FinApp.Entity;
using FinApp.EnumValue;
using FinApp.Exceptions;
using FinApp.Interfaces;
using FinApp.MiddleEntity;
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

        public async Task<ResponseType<Expense>> GetAll(int userId, MoneyFlow moneyFlow, int page, MoneyFlowSort sort)
        {
            var pageResults = 5f;
            var pageCount = Math.Ceiling(_context.expenses.Count() / pageResults);

            IQueryable<Expense> query = _context.expenses;
            query = sort switch
            {
                MoneyFlowSort.NameAsk => query.OrderBy(x => x.Name),
                MoneyFlowSort.NameDesc => query.OrderByDescending(x => x.Name),
                MoneyFlowSort.SummaryAsk => query.OrderBy(x => x.Summary),
                MoneyFlowSort.SummaryDesc => query.OrderByDescending(x => x.Summary),
                MoneyFlowSort.CategoryAsk => query.OrderBy(x => x.ExpenseCategoryId),
                MoneyFlowSort.CategoryDesc => query.OrderByDescending(x => x.ExpenseCategoryId)
            };

            var expense = await query.Where(x => x.UserId == userId && x.CreatedAt >= moneyFlow.From && x.CreatedAt <= moneyFlow.To).Skip((page - 3) * Convert.ToInt32(pageResults)).Take(Convert.ToInt32(pageResults)).ToListAsync();

            var response = new ResponseType<Expense>
            {
                ListOfType = expense,
                CurrentPage = page,
                CountPage = Convert.ToInt32(pageCount)
            };

            return response;
        }

        public async Task<Expense> Get(int id, int userId)
        {
            var expense = await _context.expenses.SingleOrDefaultAsync(x => x.Id == id);

            if (expense == null)
                throw new ExpenseNotFoudException();
            if (userId != expense.UserId)
                throw new NoAccessException();

            return expense;
        }

        public async Task<int> Create(ExpenseCreateData expenseCreateData, int userId)
        {
            var userExist = _context.user.SingleOrDefaultAsync(x => x.Id == userId);
            if (userExist != null)
                throw new UserExists();
            if (await _context.expenses.SingleOrDefaultAsync(x => x.Id == expenseCreateData.CategoryId) == null)
                throw new ExpenseNotFoudException();
            if (_context.expenses.SingleOrDefaultAsync(x => x.Id == expenseCreateData.CategoryId).Id != userId)
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

            await _context.expenses.AddAsync(newExpense);
            await _context.SaveChangesAsync();

            return newExpense.Id;
        }

        public async Task Update(ExpenseUpdateData expenseUpdateData, int userId)
        {
            var expense1 = new Expense();
            var expense = _context.expenses.SingleOrDefaultAsync(x => x.Id == expense1.Id);
            if (expense == null)
                throw new ExpenseExistException();
            if (userId != expense1.UserId)
                throw new NoAccessException();
            if (await _context.expenseCategories.SingleOrDefaultAsync(x => x.Id == expenseUpdateData.CategoryId) == null)
                throw new ExpenseNotFoudException();
            if (_context.expenseCategories.SingleOrDefault(x => x.Id == expenseUpdateData.CategoryId).UserId != userId)
                throw new NoAccessException();


            expense1.Name = expenseUpdateData.Name;
            expense1.Summary = expenseUpdateData.Summary;
            expense1.ExpenseCategoryId = expenseUpdateData.CategoryId;
            expense1.UpdatedAt = DateTime.Now;


            _context.expenses.Update(expense1);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var expenseToDelete = await _context.expenses.SingleOrDefaultAsync(x => x.Id == id);
            if (expenseToDelete == null)
            {
                throw new ExpenseIsDeletedException();
            }

            _context.expenses.Remove(expenseToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
