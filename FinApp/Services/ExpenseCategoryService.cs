using FinApp.DataBase;
using FinApp.Entity;
using FinApp.EnumValue;
using FinApp.Exceptions;
using FinApp.Interfaces;
using FinApp.MiddleEntity;
using FinApp.SearchContext;
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

        public async Task<CollectionDto<ExpenseCategories>> GetAll(CategotiesFlowSearchContext flow)
        {
            PaginationContext paginationContext = new PaginationContext { Page = flow.Page };

            IQueryable<ExpenseCategories> query = _context.ExpenseCategories;
            query = flow.Sort switch
            {
                null => query.OrderBy(x => x.Name),
                _ => query.OrderBy((SortingDirection?)flow.Sort, propertyName: flow.Sort.GetDescription())
            };

            var expenseCategory = await query.Where(x => x.UserId == flow.UserId).Skip(Convert.ToInt32(paginationContext.OffSet)).Take(paginationContext.PageSize).ToListAsync();

            var response = new CollectionDto<ExpenseCategories>
            {
                Items = expenseCategory,
                Total = _context.ExpenseCategories.Count()
            };

            return response;
        }

        public async Task<ExpenseCategories> Get(int id, int userId)
        {
            var expenseCategory = await _context.ExpenseCategories.SingleOrDefaultAsync(x => x.Id == id);

            if (expenseCategory == null)
                throw new ExpenseNotFoudException();
            if (userId != expenseCategory.UserId)
                throw new NoAccessException();

            return expenseCategory;
        }

        public async Task<int> Create(ExpenseCreateData expenseCreateData, int userId)
        {
            var userExist = _context.User.SingleOrDefaultAsync(x => x.Id == userId);
            if (userExist != null)
                throw new UserExists();
            if (await _context.ExpenseCategories.SingleOrDefaultAsync(x => x.Name == expenseCreateData.Name && x.UserId == userId) != null)
                throw new ExpenseExistException();
            var newExpenseCategory = new ExpenseCategories
            {
                Name = expenseCreateData.Name,
                UserId = userId
            };
            await _context.ExpenseCategories.AddAsync(newExpenseCategory);
            await _context.SaveChangesAsync();

            return newExpenseCategory.Id;
        }

        public async Task Update(ExpenseUpdateData expenseUpdateData, int userId)
        {
            var expenseCategory1 = new ExpenseCategories();
            var expenseCategory = _context.ExpenseCategories.SingleOrDefaultAsync(x => x.Id == expenseCategory1.Id);
            if (expenseCategory == null)
                throw new ExpenseExistException();
            if (userId != expenseCategory1.UserId)
                throw new NoAccessException();
            expenseCategory1.Name = expenseUpdateData.Name;

            _context.ExpenseCategories.Update(expenseCategory1);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var expenseCategoryToDelete = await _context.ExpenseCategories.SingleOrDefaultAsync(x => x.Id == id);
            if (expenseCategoryToDelete == null)
            {
                throw new ExpenseIsDeletedException();
            }

            _context.ExpenseCategories.Remove(expenseCategoryToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
