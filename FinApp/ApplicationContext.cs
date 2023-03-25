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
        public DbSet<User> user { get; set; }
        public DbSet<Income> income { get; set; }
        public DbSet<SourceOfIncome> sources_of_income { get; set; }
        public DbSet<Expense> expenses { get; set; }
        public DbSet<ExpenseCategory> expense_categories { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
    }
}
