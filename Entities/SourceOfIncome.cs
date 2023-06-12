using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinApp.Entities
{
    /// <summary>
    /// Represents the source of income.
    /// </summary>
    public class SourceOfIncome
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }
    }
}
