using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinApp.Controllers
{
    /// <summary>
    /// GetUserId
    /// </summary>
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Gets the id of the current user.
        /// </summary>
        /// <returns>Current user ID.</returns>
        protected int GetUserId()
        {
            return Convert.ToInt32(User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
        }
    }
}
