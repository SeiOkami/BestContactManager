using Contacts.WebClient.Models;

namespace Contacts.WebClient.Services
{
    public interface IWebAPIService
    {
        public WebAPIServiceSettings Settings { get; set; }

        public Task<ContactsModel?> ListContacts(HttpContext context);
        public Task<Stream> ExportContacts(HttpContext context);
        public Task ImportContacts(HttpContext context, ImportContactsModel model);
        public Task CreateContact(HttpContext context, ContactModel contact);
        public Task UpdateContact(HttpContext context, ContactModel contact);
        public Task<ContactModel?> GetContactAsync(HttpContext context, Guid Id);
        public Task DeleteContact(HttpContext context, Guid Id);
        public Task ClearContacts(HttpContext context);
        public Task GenerateContacts(HttpContext context);
        
    }
}
