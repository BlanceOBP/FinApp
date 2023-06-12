using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinApp.Entities
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
        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "date")]
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("ExpenseCategory")]
        public int ExpenseCategoryId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
    }
}
