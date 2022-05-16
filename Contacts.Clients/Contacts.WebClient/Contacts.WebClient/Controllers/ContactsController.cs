using Contacts.WebClient.Models;
using Contacts.WebClient.Services;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.WebClient.Controllers
{
    public class ContactsController : Controller
    {

        private readonly ITokenService _tokenService;
        private readonly IWebAPIService _webAPI;
        private readonly ILogger<HomeController> _logger;

        public ContactsController(ITokenService tokenService, ILogger<HomeController> logger, IWebAPIService webAPI)
        {
            _logger = logger;
            _tokenService = tokenService;
            _webAPI = webAPI;
        }


        [HttpGet()]
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var a = (ContactsModel)(await _webAPI.GetResultAsync(_webAPI.Settings.ListMethodURL, typeof(ContactsModel)));
            return View(a);
        }

        // GET: ContactsController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var contact = await _webAPI.GetContactAsync(id);
            if (contact == null)
                return RedirectToAction(nameof(Index));
            else
                return View(contact);
        }

        // GET: ContactsController/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        //public ActionResult Create(IFormCollection collection)
        public async Task<ActionResult> Create(ContactModel contact)
        {
            try
            {
                await _webAPI.CreateContact(contact);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: ContactsController/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(Guid id)
        {
            var contact = await _webAPI.GetContactAsync(id);
            if (contact == null)
                return RedirectToAction(nameof(Index));
            else
                return View(contact);
        }

        // POST: ContactsController/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Guid id, ContactModel contact)
        {
            try
            {
                await _webAPI.UpdateContact(contact);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: ContactsController/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var contact = await _webAPI.GetContactAsync(id);
            if (contact == null)
                return RedirectToAction(nameof(Index));
            else
                return View(contact);
        }

        // POST: ContactsController/Delete/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Guid id, IFormCollection collection)
        {
            try
            {
                await _webAPI.DeleteContact(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }
    }
}
