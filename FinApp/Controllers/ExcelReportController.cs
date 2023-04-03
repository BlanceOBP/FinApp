using FinApp.Interfaces;
using FinApp.MiddleEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinApp.Controllers
{
    [ApiController]
    [Route("excelReport")]
    public class ExcelReportController : BaseController
    {
        private readonly IExcelReportService _excelReportService;

        public ExcelReportController(IExcelReportService excelReportService)
        {
            _excelReportService = excelReportService;
        }

        /// <summary>
        /// Get income and expenses Excel report of the  user.
        /// </summary>
        /// <param name="moneyFlow">Period or time input.</param>
        /// <returns>Excel report for period of time input/ </returns>
        [Authorize(Roles = "Administrator,User")]
        [HttpPost]
        public async Task<IActionResult> GetExcelReport([FromQuery] MoneyFlow moneyFlow)
        {
            var userId = GetUserId();
            var wb = await _excelReportService.GetReport(userId, moneyFlow);

            return Ok(wb);
        }

        /// <summary>
        /// Changes excel report.
        /// </summary>
        /// <param name="report">Excel report file with changes.</param>
        [Authorize(Roles = "Administrator,User")]
        [HttpPut]
        public async Task<ActionResult> SetChangesFromExcel(IFormFile report)
        {
            var userId = GetUserId();
            await _excelReportService.SetChangesFromExcel(userId, report);

            return Ok();
        }
    }
}
