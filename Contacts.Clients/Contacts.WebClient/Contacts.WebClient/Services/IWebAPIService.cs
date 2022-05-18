using Contacts.WebClient.Models;

namespace Contacts.WebClient.Services
{
    public interface IWebAPIService
    {
        public WebAPIServiceSettings Settings { get; set; }

        public Task<ContactsModel?> ListContacts();
        public Task<Stream> ExportContacts();
        public Task ImportContacts(ImportContactsModel model);

        public Task CreateContact(ContactModel contact);

        public Task UpdateContact(ContactModel contact);

        public Task<ContactModel?> GetContactAsync(Guid Id);

        public Task DeleteContact(Guid Id);
        public Task ClearContacts();
        public Task GenerateContacts();
        
    }
}
