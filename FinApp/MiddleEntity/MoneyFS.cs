using FinApp.EnumValue;

namespace FinApp.MiddleEntity
{
    public class MoneyFS
    {
        public int UserId { get; set; }

        public int Page { get; set; }

        public MoneyFlow MoneyFlow { get; set; }

        public MoneyFlowSort Sort { get; set; }
    }
}
