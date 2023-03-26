namespace FinApp.MiddleEntity
{
    /// <summary>
    /// Represents the middleware update expense.
    /// </summary>
    public class ExpenseUpdateData
    {
        public string Name { get; set; }

        public float Summary { get; set; }

        public int CategoryId { get; set; }
    }
}
