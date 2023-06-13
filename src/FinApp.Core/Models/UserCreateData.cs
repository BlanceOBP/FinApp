using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinApp.Core.Models
{
    /// <summary>
    /// Represents the middleware create user.
    /// </summary>
    public class UserCreateData : UserUpdateData
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }


        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }
    }
}
