using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinApp.Entity
{
    /// <summary>
    /// Represents the expense category.
    /// </summary>
    public class ExpenseCategory
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
    }
}
