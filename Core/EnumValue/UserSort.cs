using System.ComponentModel;

namespace FinApp.EnumValue
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
