using FinApp.DataBase;
using FinApp.Entity;
using FinApp.Exceptions;
using FinApp.Interfaces;
using FinApp.MiddleEntity;
using Microsoft.EntityFrameworkCore;

namespace FinApp.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly ApplicationContext acb;

        public ExpenseService(ApplicationContext _acb)
        {
            acb = _acb;
        }

        public async Task<List<Expense>> GetAll(int userId, MoneyFlow moneyFlow)
        {
            var expanse = await acb.expenses.Where(x => x.User == userId && x.DateOfCreate >= moneyFlow.From && x.DateOfCreate <= moneyFlow.To).ToListAsync();
            return expanse;
        }

        public async Task<Expense> Get(int id, int userId)
        {
            var expense = await acb.expenses.SingleOrDefaultAsync(x => x.Id == id);

            if (expense == null)
                throw new ExpenseNotFoudException();
            if (userId != expense.User)
                throw new NoAccessException();

            return expense;
        }

        public async Task<int> Create(ExpenseCreateData expenseCreateData, int userId)
        {
            var userExist = acb.user.SingleOrDefaultAsync(x => x.Id == userId);
            if (userExist != null)
                throw new UserExists();
            if (await acb.expenses.SingleOrDefaultAsync(x => x.Id == expenseCreateData.CategoryId) == null)
                throw new ExpenseNotFoudException();
            if (acb.expenses.SingleOrDefaultAsync(x => x.Id == expenseCreateData.CategoryId).Id != userId)
                throw new ExpenseExistException();
            var newExpense = new Expense
            {
                Name = expenseCreateData.Name,
                Summary = expenseCreateData.Summary,
                ExpenseCategoryId = expenseCreateData.CategoryId,
                DateOfCreate = DateTime.Now,
                DateOfEdit = DateTime.Now,
                User = userId
            };
            await acb.expenses.AddAsync(newExpense);
            await acb.SaveChangesAsync();

            return newExpense.Id;
        }

        public async Task Update(ExpenseUpdateData expenseUpdateData, int userId)
        {
            var expense1 = new Expense();
            var expense = acb.expenses.SingleOrDefaultAsync(x => x.Id == expense1.Id);
            if (expense == null)
                throw new ExpenseExistException();
            if (userId != expense1.User)
                throw new NoAccessException();
            if (await acb.expenseCategories.SingleOrDefaultAsync(x => x.Id == expenseUpdateData.CategoryId) == null)
                throw new ExpenseNotFoudException();
            if (acb.expenseCategories.SingleOrDefault(x => x.Id == expenseUpdateData.CategoryId).UserId != userId)
                throw new NoAccessException();


            expense1.Name = expenseUpdateData.Name;
            expense1.Summary = expenseUpdateData.Summary;
            expense1.ExpenseCategoryId = expenseUpdateData.CategoryId;
            expense1.DateOfEdit = DateTime.Now;


            acb.expenses.Update(expense1);
            await acb.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var expenseToDelete = await acb.expenses.SingleOrDefaultAsync(x => x.Id == id);
            if (expenseToDelete == null)
            {
                throw new ExpenseIsDeletedException();
            }
            acb.expenses.Remove(expenseToDelete);
            await acb.SaveChangesAsync();
        }
    }
}
