using System.ComponentModel.DataAnnotations.Schema;

namespace FinApp.Entity
{
    public class Source
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
