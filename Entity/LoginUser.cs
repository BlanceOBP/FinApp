using System.ComponentModel.DataAnnotations;


namespace FinApp.Entity
{
    public class LoginUser
    {
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
