using FinApp.EnumValue;
using FinApp.Interfaces;
using FinApp.MiddleEntity;
using FinApp.SearchContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinApp.Controllers
{
    [ApiController]
    [Route("api/expenseCategory")]
    public class ExpenseCategoryController : BaseController
    {
        private readonly IExpenseCategoryService expenseCategoryService;

        public ExpenseCategoryController(IExpenseCategoryService _expenseCategoryService)
        {
            expenseCategoryService = _expenseCategoryService;
        }

        /// <summary>
        /// Get all expense category.
        /// </summary>
        /// <returns>List of expense category.</returns>
        /// <response code="200">Success.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="403">You don't have an access to perform this action.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [Route("get-all")]
        public async Task<IActionResult> GetList(CategotiesFlowSearchContext categotiesFlow)
        {
            categotiesFlow.UserId = GetUserId();
            var expenseCategory = await expenseCategoryService.GetAll(categotiesFlow);

            return Ok(expenseCategory);
        }

        /// <summary>
        /// Get expense category by ID.
        /// </summary>
        /// <param name="id">Desired expense category ID.</param>
        /// <returns>Expense category with the specified ID.</returns>
        /// <response code="200">Success.</response>
        /// <response code="400">Expense category with this ID was not found.</response>
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
            var expenseCategory = await expenseCategoryService.Get(id, userId);

            return Ok(expenseCategory);
        }

        /// <summary>
        /// Create expense category.
        /// </summary>
        /// <param name="expenseCreateData">Desirable create expense category.</param>
        /// <returns>Status code 200 (OK).</returns>
        /// <response code="204">Success.</response>
        [ProducesResponseType(204)]
        [HttpPost]
        [Authorize(Roles = "Administrator,User")]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] ExpenseCreateData expenseCreateData)
        {
            var userId = GetUserId();
            var expenseCategoryId = await expenseCategoryService.Create(expenseCreateData, userId);

            return CreatedAtAction(nameof(Create), expenseCategoryId);
        }

        /// <summary>
        /// Updates current expense category data.
        /// </summary>
        /// <param name="expenseUpdateData">Desirable new expense category data.</param>
        /// <returns>Status code 200 (OK).</returns>
        /// <exception cref="ExpenseCategoryNotFound">Expense category with this ID was not found.</exception>
        /// <response code="204">Success.</response>
        /// <response code="400">Expense category with this ID was not found.</response>
        /// <response code="401">Unauthorized.</response>
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Authorize(Roles = "Administrator,User")]
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] ExpenseUpdateData expenseUpdateData)
        {
            var userId = GetUserId();
            await expenseCategoryService.Update(expenseUpdateData, userId);

            return NoContent();
        }

        /// <summary>
        /// Deletes a expense category.
        /// </summary>
        /// <param name="id">Received expense category ID.</param>
        /// <returns>Status code 200 (OK).</returns>
        /// <exception cref="ExpenseCategoryNotFound">Expense category with this ID was not found.</exception>
        /// <response code="204">Success.</response>
        /// <response code="400">Expense category with this ID was not found.</response>
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
            await expenseCategoryService.Delete(id);

            return NoContent();
        }
    }
}
