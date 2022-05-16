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

        private readonly ITokenService _tokenService;
        private readonly IWebAPIService _webAPI;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ITokenService tokenService, ILogger<HomeController> logger, IWebAPIService webAPI)
        {
            _logger = logger;
            _tokenService = tokenService;
            _webAPI = webAPI;
        }

        public IActionResult Index()
        {
            //return View();
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

        [HttpGet()]
        [Authorize]
        public async Task<ActionResult<String>> Test()
        {
            return await _webAPI.TestResultAsync();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}