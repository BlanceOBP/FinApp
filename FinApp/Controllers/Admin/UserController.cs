using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FinApp.Api.Controllers.Abstractions;
using FinApp.Core.Interfaces;
using FinApp.Core.Models;
using FinApp.Core.SearchContext;

namespace FinApp.Controllers.Admin
{
    [ApiController]
    [Route("api/admin")]
    public class UserController : BaseController
    {
        private readonly IUserService userService;

        public UserController(IUserService _userService)
        {
            userService = _userService;
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <param name="page">Users list page.</param>
        /// <param name="sort">Order sorting.</param>
        /// <returns>List of all users.</returns>
        /// <response code="200">Success.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">You don't have an access to perform this action.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> GetList(UserFlowSearchContext userFlow)
        {
            var users = await userService.GetList(userFlow);

            return Ok(users);
        }

        /// <summary>
        /// Get user by ID.
        /// </summary>
        /// <param name="id">Desired user ID.</param>
        /// <returns>User with the specified ID.</returns>
        /// <response code="200">Success.</response>
        /// <response code="400">User with this ID was not found.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">You don't have an access to perform this action.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await userService.Get(id);

            return Ok(user);
        }

        /// <summary>
        /// Create user.
        /// </summary>
        /// <param name="user">Desirable create user.</param>
        /// <returns>Status code 200 (OK).</returns>
        /// <response code="204">Success.</response>
        [ProducesResponseType(204)]
        [HttpPost]
        [Authorize(Roles = "Administrator,User")]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] UserCreateData userCreateData)
        {
            var id = GetUserId();
            var userId = await userService.Create(userCreateData, id);

            return CreatedAtAction(nameof(Create), userId);
        }

        /// <summary>
        /// Updates current user data.
        /// </summary>
        /// <param name="userUpdateData">Desirable new user data.</param>
        /// <returns>Status code 200 (OK).</returns>
        /// <exception cref="UserNotFoundException">User with this ID was not found.</exception>
        /// <response code="204">Success.</response>
        /// <response code="400">User with this ID was not found.</response>
        /// <response code="401">Unauthorized.</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize(Roles = "Administrator,User")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromBody] UserUpdateData userUpdateData)
        {
            var id = GetUserId();
            await userService.Update(userUpdateData, id);

            return NoContent();
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="id">Received user ID.</param>
        /// <returns>Status code 200 (OK).</returns>
        /// <exception cref="UserNotFoundException">User with this ID was not found.</exception>
        /// <response code="204">Success.</response>
        /// <response code="400">User with this ID was not found.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">You don't have an access to perform this action.6</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await userService.Delete(id);

            return NoContent();
        }
    }
}
