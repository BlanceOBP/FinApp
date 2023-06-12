using System.ComponentModel;

namespace FinApp.Core.Enums
{
    public enum UserSort
    {
        [Description("Name")]
        Name,
        [Description("LastName")]
        LastName,
        [Description("MiddleName")]
        MiddleName,
        [Description("DateOfBirth")]
        DateOfBirth,
        [Description("Email")]
        Email
    }
}
