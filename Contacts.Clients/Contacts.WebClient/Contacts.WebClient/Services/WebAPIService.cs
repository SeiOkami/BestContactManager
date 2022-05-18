using Contacts.WebClient.Models;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace Contacts.WebClient.Services
{
    public class WebAPIService : IWebAPIService
    {
        public WebAPIServiceSettings Settings { get; set; }
        
        private ITokenService tokenService;
        
        public WebAPIService(ITokenService tokenService, IOptions<WebAPIServiceSettings> options)
        {
            this.tokenService = tokenService;
            this.Settings = options.Value;
        }        

        public async Task<ContactsModel?> ListContacts()
        {
            using var httpClient = await NewHttpClient();

            var result = await httpClient.GetAsync(Settings.ListMethodURL);
            if (result.IsSuccessStatusCode)
                return (ContactsModel?)(await result.Content.ReadFromJsonAsync(typeof(ContactsModel)));
            else
                throw new Exception(result.ToString());
        }

        public async Task<Stream> ExportContacts()
        {
            using var httpClient = await NewHttpClient();

            var result = await httpClient.GetAsync(Settings.ListMethodURL);
            if (result.IsSuccessStatusCode)
            {
                return await result.Content.ReadAsStreamAsync();
            }
            else
            {
                throw new Exception(result.ToString());
            }
        }

        public async Task CreateContact(ContactModel contact)
        {
            using var httpClient = await NewHttpClient();
            
            var json = JsonConvert.SerializeObject(contact);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(Settings.CreateMethodURL, data);

            if (!result.IsSuccessStatusCode)
                throw new Exception(result.ToString());
        }

        private async Task<HttpClient> NewHttpClient()
        {
            var tokenResponse = await tokenService.GetToken("ContactsWebClient");

            var httpClient = new HttpClient();
            httpClient.SetBearerToken(tokenResponse.AccessToken);
            
            return httpClient;
        }

        public async Task UpdateContact(ContactModel contact)
        {
            using var httpClient = await NewHttpClient();

            var fullURL = Settings.UpdateMethodURL;

            var json = JsonConvert.SerializeObject(contact);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await httpClient.PutAsync(fullURL, data);

            if (!result.IsSuccessStatusCode)
                throw new Exception(result.ToString());
        }

        public async Task<ContactModel?> GetContactAsync(Guid ID)
        {
            using var httpClient = await NewHttpClient();

            var fullURL = $"{Settings.DetailsMethodURL}{ID}";

            var result = await httpClient.GetAsync(fullURL);
            if (result.IsSuccessStatusCode)
                return (ContactModel?)(await result.Content.ReadFromJsonAsync(typeof(ContactModel)));
            else
                throw new Exception(result.ToString());
        }

        public async Task DeleteContact(Guid ID)
        {
            using var httpClient = await NewHttpClient();

            var fullURL = $"{Settings.DeleteMethodURL}{ID}";

            var result = await httpClient.DeleteAsync(fullURL);

            if (!result.IsSuccessStatusCode)
                throw new Exception(result.ToString());
        }

        public async Task ClearContacts()
        {
            using var httpClient = await NewHttpClient();

            var result = await httpClient.DeleteAsync(Settings.ClearMethodURL);

            if (!result.IsSuccessStatusCode)
                throw new Exception(result.ToString());
        }

        public async Task GenerateContacts()
        {
            using var httpClient = await NewHttpClient();

            var result = await httpClient.PostAsync(Settings.GenerateMethodURL, null);

            if (!result.IsSuccessStatusCode)
                throw new Exception(result.ToString());
        }

        public async Task ImportContacts(ImportContactsModel model)
        {

            var contacts = new StringBuilder();
            using (var reader = new StreamReader(model.FileContacts.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                    contacts.AppendLine(await reader.ReadLineAsync());
            }

            using var httpClient = await NewHttpClient();

            var content = new StringContent(contacts.ToString(), Encoding.UTF8, "application/json");

            var result = await httpClient.PostAsync(Settings.ImportMethodURL, content);

            if (!result.IsSuccessStatusCode)
                throw new Exception(result.ToString());

    }
    }
}
