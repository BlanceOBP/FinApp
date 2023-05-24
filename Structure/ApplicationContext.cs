using FinApp.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FinApp.DataBase
{
    /// <summary>
    /// Database context class
    /// </summary>
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Income> Incomes { get; set; }
        public DbSet<SourceOfIncome> SourcesOfIncomes { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
    }
}
