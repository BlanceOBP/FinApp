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
        public DbSet<Users> User { get; set; }
        public DbSet<Incomes> Income { get; set; }
        public DbSet<SourceOfIncomes> SourcesOfIncome { get; set; }
        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<ExpenseCategories> ExpenseCategories { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
    }
}
