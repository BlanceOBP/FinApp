using FinApp.Entity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace FinApp.Interface
{
    public interface IAuthService
    {
        JwtSecurityToken Authorization([FromBody] LoginData user);
        Task Registration([FromBody] RegistrationData user);
    }
}
