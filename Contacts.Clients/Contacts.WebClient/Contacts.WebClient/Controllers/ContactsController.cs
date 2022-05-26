using Contacts.WebClient.Models;
using Contacts.WebClient.Services;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Contacts.WebClient.Controllers
{
    public class ContactsController : BaseController
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
            //ContactsModel? contacts;
            try
            {
                var contacts = await _webAPI.ListContacts(HttpContext);
                return View(contacts);
            }
            catch
            {
                return RedirectToAction("Logout", "Account");
            }
        }

        [Authorize]
        public async Task<FileStreamResult> Export()
        {
            var fileStream = await _webAPI.ExportContacts(HttpContext);
            return File(fileStream, "application/json", "contacts.json");
        }
        
        [Authorize]
        public ActionResult Import()
        {
            var model = new ImportContactsModel();
            return View(model);
        }

        // POST: ContactsController/Import
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Import([FromForm] ImportContactsModel model)
        {
            try
            {
                if (model.Clear)
                    await _webAPI.ClearContacts(HttpContext);

                await _webAPI.ImportContacts(HttpContext, model);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: ContactsController/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var contact = await _webAPI.GetContactAsync(HttpContext, id);
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
        public async Task<ActionResult> Create(ContactModel contact)
        {
            try
            {
                await _webAPI.CreateContact(HttpContext, contact);
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
            var contact = await _webAPI.GetContactAsync(HttpContext, id);
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
                await _webAPI.UpdateContact(HttpContext, contact);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: ContactsController/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(Guid id)
        {
            var contact = await _webAPI.GetContactAsync(HttpContext, id);
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
                await _webAPI.DeleteContact(HttpContext, id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: ContactsController/Clear
        [Authorize]
        public ActionResult Clear()
        {
            return View();
        }

        // POST: ContactsController/Clear
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Clear(IFormCollection collection)
        {
            try
            {
                await _webAPI.ClearContacts(HttpContext);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }

        // GET: ContactsController/Generate
        [Authorize]
        public ActionResult Generate()
        {
            return View();
        }

        // POST: ContactsController/Generate
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Generate(GenerateContactsModel model)
        {
            try
            {
                if (model.Clear)
                    await _webAPI.ClearContacts(HttpContext);

                await _webAPI.GenerateContacts(HttpContext);
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
