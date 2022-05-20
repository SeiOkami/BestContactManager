using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Contacts.WebClient.Controllers
{    
    public abstract class BaseController : Controller
    {
        internal Guid UserId
        {
            get
            {
                var result = Guid.Empty;

                if (User != null && User.Identity != null
                    && User.Identity.IsAuthenticated)
                {
                    var claim = User.FindFirst(ClaimTypes.NameIdentifier);
                    if (claim != null)
                        Guid.TryParse(claim.Value, out result);
                }

                return result;
            }
        }
    }
}
