using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FinApp.Entity
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }


        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }

        public string? Email { get; set; }

        public string? Login { get; set; }


        [JsonIgnore]
        public string? Password { get; set; }


        [Column(TypeName = "date")]
        public DateTime CreateOfDate { get; set; }


        [Column(TypeName = "date")]
        public DateTime? CreateOfEdit { get; set; }

    }


}
