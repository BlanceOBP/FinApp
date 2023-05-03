using FinApp.EnumValue;
using Microsoft.AspNetCore.Mvc;

namespace FinApp.MiddleEntity
{
    public class UserFlow
    {
        public UserSort Sort { get; set; }
        
        public int Page { get; set; }
    }
}
