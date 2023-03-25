namespace FinApp.MiddleEntity
{
    /// <summary>
    /// Represents the middleware update income source.
    /// </summary>
    public class IncomeUpdateData
    {
        public string Name { get; set; }

        public float Amount { get; set; }

        public int CategoryId { get; set; }
    }
}
