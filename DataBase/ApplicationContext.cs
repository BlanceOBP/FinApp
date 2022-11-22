using FinApp.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FinApp.DataBase
{
    public class ApplicationContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"Server=localhost;Port=5433;Username=postgres;Password=a200308;Database=FinanceApp");
        }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<User> user { get; set; }
        public DbSet<Source> sources { get; set; }
        public DbSet<SourceOfIncome> sources_of_income { get; set; }
        public DbSet<Expense> expenses { get; set; }
        public DbSet<ExpenseCategory> expense_categories { get; set; }

    }
}
