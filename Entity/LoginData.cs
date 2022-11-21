using System.ComponentModel.DataAnnotations;


namespace FinApp.Entity
{
    public class LoginData
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
