using Contacts.DesctopClient.Models;
using IdentityModel.Client;
using IdentityModel.OidcClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Contacts.DesctopClient.Identity
{
    public class WebAPI
    {

        private OidcClient oidcClient;

        public UserModel User;

        private readonly string userCancelKeyError = "UserCancel";
        private readonly string accessDeniedKeyError = "access_denied";
        private readonly WebAPISettings settings;

        public WebAPI()
        {
            User = new();
            User.Name = Properties.Settings.Default.UserName;
            User.Token = Properties.Settings.Default.UserToken;

            User.IsAuthenticated = !String.IsNullOrEmpty(User.Token);

            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json").Build();

            settings = config.GetSection("SettingsWebAPI").Get<WebAPISettings>();

            var identitySettings = config.GetSection("InteractiveServiceSettings").Get<IdentityServerSettings>();

            var options = new OidcClientOptions()
            {
                Authority = identitySettings.AuthorityUrl,
                ClientId = "WPF",
                ClientSecret = "AFD9AF9D-03E1-4F54-9E57-44B334A11B78",
                Scope = "openid profile ContactsWebAPI",
                RedirectUri = "https://localhost/sigin-wpf-app-oidc",
                Browser = new IdentityBrowser(),
                PostLogoutRedirectUri = "https://localhost/signout-wpf-app-oidc"
            };

            oidcClient = new OidcClient(options);
        }

        public async Task LoginAsync()
        {
            if (User.IsAuthenticated)
                return;

            User.InProcessAuthenticated = true;

            string? error;
            try
            {
                var result = await oidcClient.LoginAsync();

                User.Token = result.AccessToken;
                User.Name = result.User?.Identity?.Name;
                User.IsAuthenticated = result.User?.Identity?.IsAuthenticated ?? false;
                error = result.Error;
            }
            catch (Exception)
            {
                throw;
            }

            if (error != null
                && error != userCancelKeyError
                && error != accessDeniedKeyError)
                MessageBox.Show(error);

            User.InProcessAuthenticated = false;
        }

        public async Task<ContactsModel?> ListContactsAsync()
        {
            using var httpClient = new HttpClientAPI(User.Token);

            var response = await httpClient.GetAsync(settings.ListMethodURL);

            if (response.IsSuccessStatusCode)
                return (ContactsModel?)(await response.Content.ReadFromJsonAsync(typeof(ContactsModel)));

            HandleResponseError(response);
            return null;

        }

        public async Task<ContactModel?> GetContactAsync(Guid ID)
        {
            using var httpClient = new HttpClientAPI(User.Token);

            var fullURL = $"{settings.DetailsMethodURL}{ID}";

            var response = await httpClient.GetAsync(fullURL);
            if (response.IsSuccessStatusCode)
                return (ContactModel?)(await response.Content.ReadFromJsonAsync(typeof(ContactModel)));

            HandleResponseError(response);
            return null;
        }

        public async Task UpdateContact(ContactModel contact)
        {
            using var httpClient = new HttpClientAPI(User.Token);

            var fullURL = settings.UpdateMethodURL;

            var json = JsonConvert.SerializeObject(contact);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(fullURL, data);

            if (!response.IsSuccessStatusCode)
                HandleResponseError(response);
        }

        public async Task<Guid?> CreateContactAsync(ContactModel contact)
        {
            using var httpClient = new HttpClientAPI(User.Token);

            var json = JsonConvert.SerializeObject(contact);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(settings.CreateMethodURL, data);

            if (response.IsSuccessStatusCode)
                return (Guid?)(await response.Content.ReadFromJsonAsync(typeof(Guid?)));

            HandleResponseError(response);
            return null;
        }

        public async Task<bool> DeleteContactAsync(Guid ID)
        {
            using var httpClient = new HttpClientAPI(User.Token);

            var fullURL = $"{settings.DeleteMethodURL}{ID}";

            var response = await httpClient.DeleteAsync(fullURL);

            if (response.IsSuccessStatusCode)
                return true;

            HandleResponseError(response);
            return false;
        }

        public async Task<Stream?> ExportContacts()
        {
            using var httpClient = new HttpClientAPI(User.Token);

            var response = await httpClient.GetAsync(settings.ListMethodURL);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadAsStreamAsync();

            HandleResponseError(response);
            return null;
        }

        public async Task ClearContactsAsync()
        {
            using var httpClient = new HttpClientAPI(User.Token);

            var response = await httpClient.DeleteAsync(settings.ClearMethodURL);
            if (!response.IsSuccessStatusCode)
                HandleResponseError(response);

        }

        public async Task GenerateContactsAsync()
        {
            using var httpClient = new HttpClientAPI(User.Token);

            var response = await httpClient.PostAsync(settings.GenerateMethodURL, null);

            if (!response.IsSuccessStatusCode)
                HandleResponseError(response);
        }

        public async Task ImportContacts(string data)
        {

            using var httpClient = new HttpClientAPI(User.Token);

            var content = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(settings.ImportMethodURL, content);

            if (!response.IsSuccessStatusCode)
                HandleResponseError(response);

        }

        public void Logout()
        {
            User.IsAuthenticated = false;
            User.InProcessAuthenticated = false;

            User.Name = String.Empty;
        }

        public void HandleResponseError(HttpResponseMessage response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                Logout();
            else
                MessageBox.Show(response.StatusCode.ToString());
        }

    }
}
