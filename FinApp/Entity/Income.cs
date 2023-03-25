using System.ComponentModel.DataAnnotations.Schema;

namespace FinApp.Entity
{
    /// <summary>
    /// Represents the income.
    /// </summary>
    public class Income
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Summary { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfCreate { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfEdit { get; set; }

        public int SourceOfIncomeId { get; set; }
    }
}
