using Contacts.WebClient.Models;
using Microsoft.AspNetCore.Mvc;
using IdentityModel.Client;
using Contacts.WebClient.Services;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace Contacts.WebClient.Controllers
{
    public class HomeController : Controller
    {

        public HomeController(){}

        public IActionResult Index()
        {
            return Redirect("Contacts");
        }
        public IActionResult Session()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}