using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FinApp.Token
{
    public class AuthOptions
    {
        const string KEY = "mysupersecret_secretkey!123";   // êëþ÷ äëÿ øèôðàöèè
        public static SymmetricSecurityKey GetSymmetricSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
