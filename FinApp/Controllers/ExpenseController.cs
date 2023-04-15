using FinApp.EnumValue;
using FinApp.Interfaces;
using FinApp.MiddleEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinApp.Controllers
{
    [ApiController]
    [Route("api/expense")]
    public class ExpenseController : BaseController
    {
        private readonly IExpenseService expenseService;

        public ExpenseController(IExpenseService _expenseService)
        {
            expenseService = _expenseService;
        }

        /// <summary>
        /// Get all expense.
        /// </summary>
        /// <param name="page">Users list page.</param>
        /// <param name="sort">Order sorting.</param>
        /// <returns>List of expense .</returns>
        /// <response code="200">Success.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">You don't have an access to perform this action.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [Route("GetAll")]
        public async Task<IActionResult> GetList(MoneyFlow moneyFlow, int page, MoneyFlowSort sort)
        {
            var userId = GetUserId();
            var expense = await expenseService.GetAll(userId, moneyFlow, page, sort);

            return Ok(expense);
        }

        /// <summary>
        /// Get expense by ID.
        /// </summary>
        /// <param name="id">Desired expense  ID.</param>
        /// <returns>Expense  with the specified ID.</returns>
        /// <response code="200">Success.</response>
        /// <response code="400">Expense  with this ID was not found.</response>
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
            var expense = await expenseService.Get(id, userId);

            return Ok(expense);
        }

        /// <summary>
        /// Create expense.
        /// </summary>
        /// <param name="expenseCreateData">Desirable create expense .</param>
        /// <returns>Status code 200 (OK).</returns>
        /// <response code="204">Success.</response>
        [ProducesResponseType(204)]
        [HttpPost]
        [Authorize(Roles = "Administrator,User")]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] ExpenseCreateData expenseCreateData)
        {
            var userId = GetUserId();
            var expenseId = await expenseService.Create(expenseCreateData, userId);

            return CreatedAtAction(nameof(Create), expenseId);
        }

        /// <summary>
        /// Updates current expense data.
        /// </summary>
        /// <param name="expenseUpdateData">Desirable new expense  data.</param>
        /// <returns>Status code 200 (OK).</returns>
        /// <exception cref="ExpenseNotFoundExcrption">Expense  with this ID was not found.</exception>
        /// <response code="204">Success.</response>
        /// <response code="400">Expense  with this ID was not found.</response>
        /// <response code="401">Unauthorized.</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize(Roles = "Administrator,User")]
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] ExpenseUpdateData expenseUpdateData)
        {
            var userId = GetUserId();
            await expenseService.Update(expenseUpdateData, userId);

            return NoContent();
        }

        /// <summary>
        /// Deletes a expense.
        /// </summary>
        /// <param name="id">Received expense  ID.</param>
        /// <returns>Status code 200 (OK).</returns>
        /// <exception cref="IncomeNotFoundEXception">Expense with this ID was not found.</exception>
        /// <response code="204">Success.</response>
        /// <response code="400">Expense with this ID was not found.</response>
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
            await expenseService.Delete(id);

            return NoContent();
        }
    }
}
