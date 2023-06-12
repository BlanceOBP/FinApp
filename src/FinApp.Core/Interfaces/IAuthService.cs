using FinApp.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace FinApp.Core.Interfaces
{
    /// <summary>
    /// Defines methods related to authorization.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Authorization a user.
        /// </summary>
        /// <param name="user">User of email and passsword.</param>
        JwtSecurityToken Authorization([FromBody] LoginData user);

        /// <summary>
        /// Registers a user.
        /// </summary>
        /// <param name="user">User registration data.</param>
        Task Registration([FromBody] RegistrationData user);
    }
}
