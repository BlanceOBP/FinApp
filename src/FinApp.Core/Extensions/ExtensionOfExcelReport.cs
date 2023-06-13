using ClosedXML.Excel;

namespace FinApp.Core.Extensions
{
    public static class ExtensionOfExcelReport
    {
        /// <summary>
        /// To report sheet adds a row.
        /// </summary>
        /// <param name="wb">Add row from WorkSheet.</param>
        /// <returns></returns>
        public static IXLWorksheet AddRow(this IXLWorksheet wb)
        {
            wb.Cell("A2").SetValue("ID");
            wb.Cell("B2").SetValue("Name");
            wb.Cell("C2").SetValue("Amount");
            wb.Cell("D2").SetValue("Date");
            wb.Range("A2:D2").Style.Font.Bold = true;

            return wb;
        }

        /// <summary>
        /// First cell set value.
        /// </summary>
        /// <param name="worksheet">Worksheet to set first cell value.</param>
        /// <param name="value">Value to set in cell.</param>
        /// <returns></returns>
        public static IXLWorksheet SetFirstCell(this IXLWorksheet worksheet, string value)
        {
            worksheet.FirstCell().SetValue(value);

            return worksheet;
        }
    }
}
