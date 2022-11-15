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
    public class AuthenticateController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="LoginException"></exception>
        /// 
               
        [Route("Authenticate")]
        [HttpGet]

        public async Task<IActionResult> Authorization([FromBody]LoginUser user)
        {
            await using var acb = new ApplicationContext();
            if (acb.users.Count(x => x.Email == user.Email && x.Password == user.Password) != 0)
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
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="InputLoginException"></exception>
        /// 
        
        [Route("Registration")]
        [HttpPost]
        public async Task<IActionResult> Registration([FromBody]RegistrationUser user)
        {
            await using var acb = new ApplicationContext();
            var Personality_of_the_login = acb.users.Count(x => x.Login == user.Login) == 1;

            try
            {
                if (Personality_of_the_login == true)
                {
                    throw new InputLoginException();
                }
                else
                {
                    var dateOfCreate = DateTime.Today;
                    var DateOfEdit = DateTime.Today;

                    var NewUser = new Users { Name = user.Name, LastName = user.LastName, MiddleName = user.MiddleName, DateOfBirth = Convert.ToDateTime(user.DateOfBirth) , Email = user.Email, Login = user.Login, CreateOfDate = dateOfCreate, CreateOfEdit = DateOfEdit };
                    acb.users.Add(NewUser);
                    await acb.SaveChangesAsync();
                    return (StatusCode(200));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}