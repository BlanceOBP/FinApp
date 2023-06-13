using System.ComponentModel.DataAnnotations;


namespace FinApp.Core.Models
{
    /// <summary>
    /// Represents the login information.
    /// </summary>
    public class LoginData
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
