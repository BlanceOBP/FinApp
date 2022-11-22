using FinApp.DataBase;
using FinApp.Entity;
using FinApp.Exceptions;
using FinApp.Token;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FinApp.Controllers
{

    [ApiController]
    [Route("api/auth")]
    public class AuthAndReg : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user">User Email and Password</param>
        /// <returns></returns>
        /// <exception cref="LoginException">Login or password entered incorrectly.</exception>       
        [Route("auth")]
        [HttpPost]
        public async Task<IActionResult> Authorization([FromBody]LoginData user)
        {
            await using var acb = new ApplicationContext();
            if (acb.user.Count(x => x.Email == user.Email && x.Password == user.Password) != 0)
            {
                throw new LoginException();
            }
            var claims = new List<Claim> { new(ClaimTypes.Name, user.Email) };
            var jwt = new JwtSecurityToken(
            claims: claims,
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user">User Login, Name, Email, MiddleName, LastName, DateOfBirth and Password</param>
        /// <returns></returns>
        /// <exception cref="InputLoginException">Email or login already in use. Please enter new email or login.</exception>
        [Route("registration")]
        [HttpPost]
        public async Task<IActionResult> Registration([FromBody]RegistrationData user)
        {
            await using var acb = new ApplicationContext();

            if (acb.user.SingleOrDefault(x => x.Login == user.Login) != null || acb.user.SingleOrDefault(x => x.Email == user.Email) != null)
            {
                throw new InputLoginException();
            }

            var dateOfCreate = DateTime.Today;

            var newUser = new User { Name = user.Name, LastName = user.LastName, MiddleName = user.MiddleName, DateOfBirth = Convert.ToDateTime(user.DateOfBirth) , Email = user.Email, Login = user.Login, CreateOfDate = dateOfCreate };

            acb.user.Add(newUser);
            await acb.SaveChangesAsync();

            return Ok();

        }
    }
}