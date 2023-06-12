using FinApp.Core.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FinApp.Core.SearchContext
{
    public class UserFlowSearchContext
    {
        public UserSort? Sort { get; set; }

        public int Page { get; set; }
    }
}
