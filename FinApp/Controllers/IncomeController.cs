using FinApp.EnumValue;
using FinApp.Interfaces;
using FinApp.MiddleEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinApp.Controllers
{
    [ApiController]
    [Route("api/income")]
    public class IncomeController : BaseController
    {
        private readonly IIncomeService incomeService;

        public IncomeController(IIncomeService _incomeService)
        {
            incomeService = _incomeService;
        }

        /// <summary>
        /// Get all income.
        /// </summary>
        /// <param name="page">Users list page.</param>
        /// <param name="sort">Order sorting.</param>
        /// <returns>List of income .</returns>
        /// <response code="200">Success.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">You don't have an access to perform this action.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [Route("get-all")]
        public async Task<IActionResult> GetList(MoneyFSSearchContext moneyFS)
        {
            moneyFS.UserId = GetUserId();
            var income = await incomeService.GetAll(moneyFS);

            return Ok(income);
        }

        /// <summary>
        /// Get income by ID.
        /// </summary>
        /// <param name="id">Desired income  ID.</param>
        /// <returns>Income  with the specified ID.</returns>
        /// <response code="200">Success.</response>
        /// <response code="400">Income  with this ID was not found.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">You don't have an access to perform this action.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [Route("get")]
        public async Task<IActionResult> Get(int id)
        {
            var userId = GetUserId();
            var income = await incomeService.Get(id, userId);

            return Ok(income);
        }

        /// <summary>
        /// Create income.
        /// </summary>
        /// <param name="incomeCreateData">Desirable create income .</param>
        /// <returns>Status code 200 (OK).</returns>
        /// <response code="204">Success.</response>
        [ProducesResponseType(204)]
        [HttpPost]
        [Authorize(Roles = "Administrator,User")]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] IncomeCreateData incomeCreateData)
        {
            var userId = GetUserId();
            var incomeId = await incomeService.Create(incomeCreateData, userId);

            return CreatedAtAction(nameof(Create), incomeId);
        }

        /// <summary>
        /// Updates current income  data.
        /// </summary>
        /// <param name="incomeUpdateData">Desirable new income  data.</param>
        /// <returns>Status code 200 (OK).</returns>
        /// <exception cref="IncomeNotFoundExcrption">Income  with this ID was not found.</exception>
        /// <response code="204">Success.</response>
        /// <response code="400">Income  with this ID was not found.</response>
        /// <response code="401">Unauthorized.</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize(Roles = "Administrator,User")]
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] IncomeUpdateData incomeUpdateData)
        {
            var userId = GetUserId();
            await incomeService.Update(incomeUpdateData, userId);

            return NoContent();
        }

        /// <summary>
        /// Deletes a income.
        /// </summary>
        /// <param name="id">Received income  ID.</param>
        /// <returns>Status code 200 (OK).</returns>
        /// <exception cref="IncomeNotFoundEXception">Income with this ID was not found.</exception>
        /// <response code="204">Success.</response>
        /// <response code="400">Income with this ID was not found.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">You don't have an access to perform this action.6</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await incomeService.Delete(id);

            return NoContent();
        }
    }
}
