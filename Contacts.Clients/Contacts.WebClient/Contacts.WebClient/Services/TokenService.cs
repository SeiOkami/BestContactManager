using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Contacts.WebClient.Services
{
    public class TokenService : ITokenService
    {

        public TokenService(){}

        public async Task<string> GetToken(HttpContext httpContext)
        {
            var authResult = await httpContext.AuthenticateAsync();

            var access_token = authResult?.Ticket?.Properties.GetTokenValue("access_token");

            if (access_token == null)
                throw new Exception("Not access token");

            return access_token;
        }

    }
}