using System.ComponentModel.DataAnnotations;

namespace FinApp.MiddleEntity
{
    /// <summary>
    /// Represents the middleware update user.
    /// </summary>
    public class UserUpdateData
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Login { get; set; }
    }
}
