using System.ComponentModel.DataAnnotations;

namespace FinApp.Entity
{
    /// <summary>
    /// Represents the register information.
    /// </summary>
    public class RegistrationData
    {
        public string Name { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string DateOfBirth { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }
    }
}
