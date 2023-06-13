using FinApp.Core.Exceptions;
using FinApp.Core.Interfaces;
using FinApp.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinApp.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticateController(IAuthService authService)
        {
            _authService = authService;
        }


        /// <summary>
        /// </summary>
        /// <param name="user">Password and email of the user</param>
        /// <returns>User JWT Token, Status Code 200 (OK)</returns>
        /// <exception cref="LoginException">Incorrect data</exception>
        /// <response code="200">User successfully logged in</response>
        /// <response code="400">Incorrect data</response>
        [Route("login")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Authorize([FromBody] LoginData user)
        {
            var token = _authService.Authorization(user);

            return Ok(token);
        }

        /// <summary>
        /// </summary>
        /// <param name="user">Login, name, email, middle name, last name, birth date and password of the user</param>
        /// <returns>Status Code 200 (OK)</returns>
        /// <exception cref="InputLoginException">Login or email is taken</exception>
        /// <response code="200">Registration is successfully</response>
        /// <response code="400">Login or email is taken</response> 
        [Route("register")]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Register([FromBody] RegistrationData user)
        {
            await _authService.Registration(user);

            return Ok();
        }
    }
}