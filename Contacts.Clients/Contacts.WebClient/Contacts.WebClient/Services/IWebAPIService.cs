using Contacts.WebClient.Models;

namespace Contacts.WebClient.Services
{
    public interface IWebAPIService
    {
        public WebAPIServiceSettings Settings { get; set; }

        public Task<object?> GetResultAsync(string methodURL, Type typeResult, string additionalURL = "");
        
        public async Task<string?> TestResultAsync()
        {
            return (string?)(await GetResultAsync(Settings.MainURL + "Test", typeof(string)));
        }
        public Task CreateContact(ContactModel contact);

        public Task UpdateContact(ContactModel contact);

        public Task<ContactModel?> GetContactAsync(Guid Id);

        public Task DeleteContact(Guid Id);
        public Task ClearContacts();


    }
}
