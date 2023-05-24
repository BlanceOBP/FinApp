using ClosedXML.Excel;
using FinApp.SearchContext;
using Microsoft.AspNetCore.Http;

namespace FinApp.Interfaces
{
    public interface IExcelReportService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">Current user ID.</param>
        /// <param name="moneyFlow">Period of time input.</param>
        /// <returns></returns>
        Task<XLWorkbook> GetReport(int userId, MoneyFlowSearchContext moneyFlow);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">Current user ID.</param>
        /// <param name="report">Excel report file.</param>
        /// <returns></returns>
        Task SetChangesFromExcel(int userId, IFormFile report);
    }
}
