using FinApp.EnumValue;

namespace FinApp.SearchContext
{
    public class CategotiesFlowSearchContext
    {
        public int UserId { get; set; }

        public int Page { get; set; }

        public CategotiesSort? Sort { get; set; }

    }
}
