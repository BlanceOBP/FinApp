using FinApp.EnumValue;
using Microsoft.AspNetCore.Mvc;

namespace FinApp.SearchContext
{
    public class UserFlowSearchContext
    {
        public UserSort? Sort { get; set; }

        public int Page { get; set; }
    }
}
