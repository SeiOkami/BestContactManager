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

        public async Task<object?> GetResultAsync(string methodURL, Type typeResult, string additionalURL = "")
        {
            var tokenResponse = await tokenService.GetToken("ContactsWebClient");

            using var httpClient = new HttpClient(); 
            httpClient.SetBearerToken(tokenResponse.AccessToken);

            var fullURL = methodURL;
            if (additionalURL != "")
                fullURL += $"/{additionalURL}";

            var result = await httpClient.GetAsync(fullURL);
            if (result.IsSuccessStatusCode)
                return (await result.Content.ReadFromJsonAsync(typeResult));
            else
                throw new Exception(result.ToString());
        }

        public async Task CreateContact(ContactModel contact)
        {
            var tokenResponse = await tokenService.GetToken("ContactsWebClient");

            using var httpClient = new HttpClient();
            httpClient.SetBearerToken(tokenResponse.AccessToken);

            //var obj = new { FirstName = contact.FirstName };
            var obj = contact;
            var json = JsonConvert.SerializeObject(obj);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await httpClient.PostAsync(Settings.CreateMethodURL, data);

            if (!result.IsSuccessStatusCode)
                throw new Exception(result.ToString());
        }


        public async Task UpdateContact(ContactModel contact)
        {
            var tokenResponse = await tokenService.GetToken("ContactsWebClient");

            using var httpClient = new HttpClient();
            httpClient.SetBearerToken(tokenResponse.AccessToken);

            //var fullURL = $"{Settings.UpdateMethodURL}{contact.Id}";
            var fullURL = Settings.UpdateMethodURL;

            var json = JsonConvert.SerializeObject(contact);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await httpClient.PutAsync(fullURL, data);

            if (!result.IsSuccessStatusCode)
                throw new Exception(result.ToString());
        }


        public async Task<ContactModel?> GetContactAsync(Guid ID)
        {
            var tokenResponse = await tokenService.GetToken("ContactsWebClient");

            using var httpClient = new HttpClient();
            httpClient.SetBearerToken(tokenResponse.AccessToken);

            var fullURL = $"{Settings.DetailsMethodURL}{ID}";

            var result = await httpClient.GetAsync(fullURL);
            if (result.IsSuccessStatusCode)
                return (ContactModel?)(await result.Content.ReadFromJsonAsync(typeof(ContactModel)));
            else
                throw new Exception(result.ToString());
        }


        public async Task DeleteContact(Guid ID)
        {
            var tokenResponse = await tokenService.GetToken("ContactsWebClient");

            using var httpClient = new HttpClient();
            httpClient.SetBearerToken(tokenResponse.AccessToken);

            //var fullURL = $"{Settings.UpdateMethodURL}{contact.Id}";
            var fullURL = $"{Settings.DeleteMethodURL}{ID}";

            var result = await httpClient.DeleteAsync(fullURL);

            if (!result.IsSuccessStatusCode)
                throw new Exception(result.ToString());
        }



        public async Task ClearContacts()
        {
            var tokenResponse = await tokenService.GetToken("ContactsWebClient");

            using var httpClient = new HttpClient();
            httpClient.SetBearerToken(tokenResponse.AccessToken);

            var result = await httpClient.DeleteAsync(Settings.ClearMethodURL);

            if (!result.IsSuccessStatusCode)
                throw new Exception(result.ToString());
        }


        public async Task GenerateContacts()
        {
            var tokenResponse = await tokenService.GetToken("ContactsWebClient");

            using var httpClient = new HttpClient();
            httpClient.SetBearerToken(tokenResponse.AccessToken);

            var result = await httpClient.PostAsync(Settings.GenerateMethodURL, null);

            if (!result.IsSuccessStatusCode)
                throw new Exception(result.ToString());
        }

        //public async Task<object?> SendAsync(string methodURL, Object typeResult, string additionalURL = "")
        //{
        //    var tokenResponse = await tokenService.GetToken("ContactsWebClient");

        //    using var httpClient = new HttpClient();
        //    httpClient.SetBearerToken(tokenResponse.AccessToken);

        //    var fullURL = methodURL;
        //    if (additionalURL != "")
        //        fullURL += $"/{additionalURL}";

        //    var json = JsonConvert.SerializeObject(person);
        //    var data = new StringContent(json, Encoding.UTF8, "application/json");
        //    httpClient.PostAsync(fullURL, )

        //    var result = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Post, fullURL));
        //    if (result.IsSuccessStatusCode)
        //        return (await result.Content.ReadFromJsonAsync(typeResult));
        //    else
        //        throw new Exception(result.ToString());
        //}

    }
}
