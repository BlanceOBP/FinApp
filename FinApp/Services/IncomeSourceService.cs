﻿using FinApp.Controllers;
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
    public class IncomeSourceService : IIncomeSourceService
    {
        private readonly ApplicationContext _context;

        public IncomeSourceService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<CollectionDto<SourceOfIncomes>> GetAll(CategotiesFlowSearchContext sort)
        {
            PaginationContext paginationContext = new PaginationContext { Page = sort.Page };

            IQueryable<SourceOfIncomes> query = _context.SourcesOfIncome;
            query = sort.Sort switch
            {
                null => query.OrderBy(x => x.Name),
                _ => query.OrderBy((SortingDirection?)sort.Sort, propertyName: sort.Sort.GetDescription())
            };

            var incomeSources =  await query.Where(x => x.UserId == sort.UserId).Skip(Convert.ToInt32(paginationContext.OffSet)).Take(paginationContext.PageSize).ToListAsync();

            var response = new CollectionDto<SourceOfIncomes>
            {
                Items = incomeSources,
                Total = _context.SourcesOfIncome.Count()
            };

            return response;
        }

        public async Task<SourceOfIncomes> Get(int id, int userId)
        {
            var incomeSource = await _context.SourcesOfIncome.SingleOrDefaultAsync(x => x.Id == id);

            if (incomeSource == null)
                throw new IncomeSourceNotFound();
            if (userId != incomeSource.UserId)
                throw new NoAccessException();
            
            return incomeSource;
        }

        public async Task<int> Create(IncomeCreateData incomeCreateData, int userId)
        {
            var userExist = _context.User.SingleOrDefaultAsync(x => x.Id == userId);
            if (userExist != null)
                throw new UserExists();
            if (await _context.SourcesOfIncome.SingleOrDefaultAsync(x => x.Name == incomeCreateData.Name && x.UserId == userId) != null)
                throw new IncomeSourceExistException();
            var newIncomeSource = new SourceOfIncomes
            {
                Name = incomeCreateData.Name,
                UserId = userId
            };

            await _context.SourcesOfIncome.AddAsync(newIncomeSource);
            await _context.SaveChangesAsync();

            return newIncomeSource.Id;
        }

        public async Task Update(IncomeUpdateData incomeUpdateData, int userId)
        {
            var incomeSource1 = new SourceOfIncomes();
            var incomeSource = _context.SourcesOfIncome.SingleOrDefaultAsync(x => x.Id == incomeSource1.Id);
            if (incomeSource == null)
                throw new IncomeSourceExistException();
            if (userId != incomeSource1.UserId)
                throw new NoAccessException();
            incomeSource1.Name = incomeUpdateData.Name;

            _context.SourcesOfIncome.Update(incomeSource1);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var incomeSourceToDelete = await _context.SourcesOfIncome.SingleOrDefaultAsync(x => x.Id == id);
            if (incomeSourceToDelete == null)
            {
                throw new IncomeIsDeletedException();
            }

            _context.SourcesOfIncome.Remove(incomeSourceToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
