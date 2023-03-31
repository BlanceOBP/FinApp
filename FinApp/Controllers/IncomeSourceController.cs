using FinApp.Interfaces;
using FinApp.MiddleEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinApp.Controllers
{
    [ApiController]
    [Route("api/incomeSource")]
    public class IncomeSourceController : BaseController
    {
        private readonly IIncomeSourceService incomeSourceService;

        public IncomeSourceController(IIncomeSourceService _incomeSourceService)
        {
            incomeSourceService = _incomeSourceService;
        }

        /// <summary>
        /// Get all income source.
        /// </summary>
        /// <returns>List of income source.</returns>
        /// <response code="200">Success.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">You don't have an access to perform this action.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [Route("GetAll")]
        public async Task<IActionResult> GetList()
        {
            var userId = GetUserId();
            var incomeSource = await incomeSourceService.GetAll(userId);

            return Ok(incomeSource);
        }

        /// <summary>
        /// Get income source by ID.
        /// </summary>
        /// <param name="id">Desired income source ID.</param>
        /// <returns>Income source with the specified ID.</returns>
        /// <response code="200">Success.</response>
        /// <response code="400">Income source with this ID was not found.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">You don't have an access to perform this action.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var userId = GetUserId();
            var incomeSource = await incomeSourceService.Get(id, userId);

            return Ok(incomeSource);
        }

        /// <summary>
        /// Create income source.
        /// </summary>
        /// <param name="incomeCreateData">Desirable create income source.</param>
        /// <returns>Status code 200 (OK).</returns>
        /// <response code="204">Success.</response>
        [ProducesResponseType(204)]
        [HttpPost]
        [Authorize(Roles = "Administrator,User")]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] IncomeCreateData incomeCreateData)
        {
            var userId = GetUserId();
            var incomeSourceId = await incomeSourceService.Create(incomeCreateData, userId);

            return CreatedAtAction(nameof(Create), incomeSourceId);
        }

        /// <summary>
        /// Updates current income source data.
        /// </summary>
        /// <param name="incomeUpdateData">Desirable new income source data.</param>
        /// <returns>Status code 200 (OK).</returns>
        /// <exception cref="IncomeSourceNotFound">Income source with this ID was not found.</exception>
        /// <response code="204">Success.</response>
        /// <response code="400">Income source with this ID was not found.</response>
        /// <response code="401">Unauthorized.</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize(Roles = "Administrator,User")]
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] IncomeUpdateData incomeUpdateData)
        {
            var userId = GetUserId();
            await incomeSourceService.Update(incomeUpdateData, userId);

            return NoContent();
        }

        /// <summary>
        /// Deletes a income source.
        /// </summary>
        /// <param name="id">Received income source ID.</param>
        /// <returns>Status code 200 (OK).</returns>
        /// <exception cref="IncomeSourceNotFound">Income source with this ID was not found.</exception>
        /// <response code="204">Success.</response>
        /// <response code="400">Income source with this ID was not found.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">You don't have an access to perform this action.6</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await incomeSourceService.Delete(id);

            return NoContent();
        }
    }
}
