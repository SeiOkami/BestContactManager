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
        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("ContactsWebAPI", "Web API"),
                new ApiScope("ContactsWebClient", "Web API"),
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("ContactsWebAPI", "Web API", new []
                    { JwtClaimTypes.Name})
                {
                    Scopes = { "ContactsWebAPI", "ContactsWebClient" },
                    ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())}
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "ContactsWebAPI",
                    ClientName = "Contacts Web",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret("SuperSecretPassword".Sha256())},

                    AllowedScopes = { "ContactsWebAPI", "ContactsWebClient" }

                },
                new Client
                {
                    ClientId = "ContactsWebClient",
                    ClientSecrets = { new Secret("SuperSecretPassword".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    AllowOfflineAccess = true,
                    RequireConsent = true,
                    RequirePkce = true,
                    AllowPlainTextPkce = false,

                    // where to redirect to after login
                    RedirectUris = { "https://localhost:5444/signin-oidc" },
                    
                    FrontChannelLogoutUri = "https://localhost:5444/signout-oidc",

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:5444/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "ContactsWebClient"
                    }
                }

            };

        public static List<TestUser> TestUsers 
        {            
            get
            {
                var address = new
                {
                    street_address = "One Hacker Way",
                    locality = "Heidelberg",
                    postal_code = 69118,
                    country = "Germany"
                };

                return new List<TestUser>
                {
                    new TestUser
                    {
                        SubjectId = "818727",
                        Username = "alice",
                        Password = "alice",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.Name, "Alice Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Alice"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                            new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)
                        }
},
                    new TestUser
                    {
                        SubjectId = "88421113",
                        Username = "bob",
                        Password = "bob",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.Name, "Bob Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Bob"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                            new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)
                        }
                    }
                };
            }
        }


    }

}
