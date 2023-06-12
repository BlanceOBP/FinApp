using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinApp.Entities
{
    /// <summary>
    /// Represents the income.
    /// </summary>
    public class Income
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [Column(TypeName = "money")]
        public float Summary { get; set; }

        [Column(TypeName = "date")]
        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "date")]
        public DateTime UpdatedAt { get; set; }

        [ForeignKey("SourceOfIncome")]
        public int SourceOfIncomeId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
    }
}
