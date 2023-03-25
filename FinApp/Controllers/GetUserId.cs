using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinApp.Controllers
{
    /// <summary>
    /// GetUserId
    /// </summary>
    public class GetUserId : ControllerBase
    {
        /// <summary>
        /// Gets the id of the current user.
        /// </summary>
        /// <returns>Current user ID.</returns>
        protected int GetId()
        {
            return Convert.ToInt32(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}
