using Microsoft.AspNetCore.Mvc;
using Contacts.WebClient.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.WebClient.Controllers
{
    public class AccountController : Controller
    {

        
        /// <summary>
        /// Show logout page
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            return await Logout();
        }


        /// <summary>
        /// Handle logout page postback
        /// </summary>
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            var cookies = HttpContext.Response.Cookies;
            cookies.Delete("idsrv.session");
            cookies.Delete(".AspNetCore.Identity.Application");

            return Redirect("~/");
        }



    }

}