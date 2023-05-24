using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FinApp.Token
{
    /// <summary>
    /// Jwt token
    /// </summary>
    public class AuthOptions
    {
        const string KEY = "mysupersecret_secretkey!123";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
