using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Test;

namespace Contacts.Identity
{
    public static class Configuration
    {

        public static readonly string UserRoleName = "User";
        public static readonly string AdminRoleName = "Admin";
        public static readonly string ApiScopeName = "ContactsWebAPI";
        public static readonly string ClientName = "ContactsWebAPI";
        public static readonly string ApiResourceName = "ContactsWebAPI";
        public static readonly string SecretPassword = "AFD9AF9D-03E1-4F54-9E57-44B334A11B78";

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope(ApiScopeName)
                {
                    UserClaims =
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Subject,
                        JwtClaimTypes.Id,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Role,
                    }
                }
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
                new IdentityResources.Phone()
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource(ApiResourceName)
                {
                    Scopes = {
                        ApiScopeName,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },
                },
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = ClientName,
                    ClientName = ClientName,

                    ClientSecrets = { new Secret(SecretPassword.Sha256()) },
                    AllowedGrantTypes =
                    {
                        GrantType.AuthorizationCode,
                        GrantType.ClientCredentials
                    },

                    RequirePkce = true,
                    RedirectUris = { "https://localhost:5444/signin-oidc" },

                    FrontChannelLogoutUri = "https://localhost:5444/signout-oidc",
                    AlwaysIncludeUserClaimsInIdToken = true,

                    PostLogoutRedirectUris = { "https://localhost:5444/signout-callback-oidc" },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        ApiScopeName
                    },
                    AllowAccessTokensViaBrowser = true
                },

                new Client()
                {

                    ClientId = "WPF",
                    ClientName = "WPF",

                    AllowedCorsOrigins = { "https://localhost" },

                    RedirectUris = { "https://localhost/sigin-wpf-app-oidc" },
                    PostLogoutRedirectUris = { "https://localhost/signout-wpf-app-oidc" },
                    RequireClientSecret = true,
                    RequireConsent = false,
                    ClientSecrets = { new Secret(SecretPassword.Sha256()) },
                    //AllowedGrantTypes =
                    //{
                    //    GrantType.AuthorizationCode,
                    //    GrantType.ClientCredentials
                    //},
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,                    
                    //AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        //IdentityServerConstants.StandardScopes.OfflineAccess,
                        ApiScopeName
                    },
                    AlwaysIncludeUserClaimsInIdToken = true,
                    //IdentityTokenLifetime=3600,
                    AllowOfflineAccess = true,
                    AllowAccessTokensViaBrowser = true
                }


            };

    }

}
