using Contacts.WebClient.Models;
using Microsoft.AspNetCore.Mvc;
using IdentityModel.Client;
using Contacts.WebClient.Services;
using System.Diagnostics;

namespace Contacts.WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITokenService _tokenService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ITokenService tokenService, ILogger<HomeController> logger)
        {
            _logger = logger;
            _tokenService = tokenService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("Test")]
        public async Task<ActionResult<String>> Test()
        {
            using (var client = new HttpClient())
            {
                var tokenResponse = await _tokenService.GetToken("ContactsWebClient");

                client.SetBearerToken(tokenResponse.AccessToken);

                var result = client
                  .GetAsync("https://localhost:7058/api/contact/Test")
                  .Result;

                if (result.IsSuccessStatusCode)
                {
                    return result.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    throw new Exception("Unable to get content");
                }
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}