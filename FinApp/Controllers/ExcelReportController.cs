using FinApp.Controllers.Abstractions;
using FinApp.Interfaces;
using FinApp.SearchContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinApp.Controllers
{
    [ApiController]
    [Route("excel-report")]
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
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] MoneyFlowSearchContext moneyFlow)
        {
            var userId = GetUserId();
            var report = await _excelReportService.GetReport(userId, moneyFlow);

            return Ok(report);
        }

        /// <summary>
        /// Changes excel report.
        /// </summary>
        /// <param name="report">Excel report file with changes.</param>
        [Authorize(Roles = "Administrator,User")]
        [HttpPut]
        public async Task<ActionResult> SetChanges(IFormFile report)
        {
            var userId = GetUserId();
            await _excelReportService.SetChangesFromExcel(userId, report);

            return Ok();
        }
    }
}
