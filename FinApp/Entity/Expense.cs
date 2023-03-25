using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinApp.Entity
{
    /// <summary>
    /// Represents the expense.
    /// </summary>
    public class Expense
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public float Summary { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfCreate { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfEdit { get; set; }

        [ForeignKey("ExpenseCategory")]
        public int ExpenseCategoryId { get; set; }
    }
}
