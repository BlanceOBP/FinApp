using FinApp.Core.Enums;

namespace FinApp.Core.SearchContext
{
    public class MoneySearchContext
    {
        public int UserId { get; set; }

        public int Page { get; set; }

        public MoneyFlowSearchContext MoneyFlow { get; set; }

        public MoneyFlowSort? Sort { get; set; }
    }
}
