using FinApp.Core.Enums;

namespace FinApp.Core.SearchContext
{
    public class CategotiesFlowSearchContext
    {
        public int UserId { get; set; }

        public int Page { get; set; }

        public CategotiesSort? Sort { get; set; }

    }
}
