using ClosedXML.Excel;
using FinApp.DataBase;
using FinApp.Interfaces;
using FinApp.MiddleEntity;

namespace FinApp.Services
{
    public class ExcelReportService : IExcelReportService
    {
        private readonly ApplicationContext _context;

        public ExcelReportService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<XLWorkbook> GetReport(int userId, MoneyFlowSearchContext moneyFlow)
        {
            using var workBook = new XLWorkbook();

            var wb = new XLWorkbook();

            var incomeSheet = wb.AddWorksheet("Income").SetFirstCell($"Income from {moneyFlow.From} to {moneyFlow.To}")
            .AddRow();
            var incomeList = _context.Income.Where(x => x.UserId == userId && x.CreatedAt >= moneyFlow.From && x.CreatedAt <= moneyFlow.To).OrderBy(x => x.Id).Select(x => new { x.Id, x.Name, x.Summary, x.CreatedAt });
            incomeSheet.Column(1).Width = 20;
            incomeSheet.Columns().AdjustToContents(2, incomeList.Count() + 2);

            var incomeStartingPoint = incomeSheet.Cell("A3").InsertData(incomeList);
            incomeStartingPoint.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

            var incomeTableRange = incomeSheet.Range($"A2:D{incomeList.Count() + 2}");
            incomeTableRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            incomeTableRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            var expenseSheet = wb.AddWorksheet("Expense").SetFirstCell($"Expense from {moneyFlow.From} to {moneyFlow.To}").AddRow();
            var expenseList = _context.Expenses.Where(x => x.UserId == userId && x.CreatedAt >= moneyFlow.From && x.CreatedAt <= moneyFlow.To).OrderBy(x => x.Id).Select(x => new { x.Id, x.Name, x.Summary, x.CreatedAt });
            expenseSheet.Column(1).Width = 20;
            expenseSheet.Columns().AdjustToContents(2, expenseList.Count() + 2);

            var expenseStartingPoint = expenseSheet.Cell("A3").InsertData(expenseList);
            expenseStartingPoint.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

            var expenseTableRange = expenseSheet.Range($"A2:D{expenseList.Count() + 2}");
            expenseTableRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            expenseTableRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            return await Task.FromResult(wb);
        }

        public async Task SetChangesFromExcel(int userId, IFormFile report)
        {
            await using var reportPath = report.OpenReadStream();
            var wb = new XLWorkbook();

            var expenseSheet = wb.Worksheet("Expenses");
            var expenseLastRowNumber = expenseSheet.LastCellUsed().Address.RowNumber;

            for (var expenseRowNumber = 3; expenseRowNumber <= expenseLastRowNumber; expenseRowNumber++)
            {
                var id = Convert.ToInt32(expenseSheet.Cell($"A{expenseRowNumber}").GetString());
                var name = expenseSheet.Cell($"B{expenseRowNumber}").GetString();
                var summary = Convert.ToSingle(expenseSheet.Cell($"C{expenseRowNumber}").GetString());
                var date = expenseSheet.Cell($"D{expenseRowNumber}").GetDateTime();

                var expensesRecordNow = _context.Expenses.SingleOrDefault(x => x.Id == id);
                if (expensesRecordNow.UserId == userId)
                {
                    expensesRecordNow.Name = name;
                    expensesRecordNow.Summary = summary;
                    expensesRecordNow.CreatedAt = date;
                    expensesRecordNow.UpdatedAt = DateTime.Now;
                }
            }

            var incomeSheet = wb.Worksheet("Income");
            var incomeLastRowNumber = incomeSheet.LastCellUsed().Address.RowNumber;

            for (var incomeRowNumber = 3; incomeRowNumber <= incomeLastRowNumber; incomeRowNumber++)
            {
                var id = Convert.ToInt32(incomeSheet.Cell($"A{incomeRowNumber}").GetString());
                var name = incomeSheet.Cell($"B{incomeRowNumber}").GetString();
                var amount = Convert.ToSingle(incomeSheet.Cell($"C{incomeRowNumber}").GetString());
                var date = incomeSheet.Cell($"D{incomeRowNumber}").GetDateTime();

                var incomeRecordNow = _context.Income.SingleOrDefault(x => x.Id == id);
                if (incomeRecordNow.UserId == userId)
                {
                    incomeRecordNow.Name = name;
                    incomeRecordNow.Summary = amount;
                    incomeRecordNow.CreatedAt = date;
                    incomeRecordNow.UpdatedAt = DateTime.Now;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}
