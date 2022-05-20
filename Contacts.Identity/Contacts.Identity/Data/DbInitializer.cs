using System;
using System.Linq;
using System.Security.Claims;
using Contacts.Identity.Data;
using Contacts.Identity.Models;
using IdentityModel;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.EntityFramework.Storage;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Contacts.Identity.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            if (scope == null)
                throw new Exception(nameof(scope));

            var serviseScope = scope.ServiceProvider;
            serviseScope.GetService<PersistedGrantDbContext>()?.Database.Migrate();

            var context = serviseScope.GetService<ConfigurationDbContext>();

            if (context == null)
            {
                throw new Exception(nameof(context));
            }
            else
            {
                context.Database.Migrate();
                EnsureSeedData(context);
            }

            var authContext = serviseScope.GetService<AuthDbContext>();
            authContext?.Database.Migrate();
            SeedRoles(scope);
            EnsureUsers(scope);
        }

        public static void SeedRoles(IServiceScope scope)           
        {

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (roleManager.Roles.Any())
                return;

            roleManager.CreateAsync(new IdentityRole(Configuration.AdminRoleName));
            roleManager.CreateAsync(new IdentityRole(Configuration.UserRoleName));
            
            Log.Debug("roles created");
        }

        private static void EnsureUsers(IServiceScope scope)
        {

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            if (userManager.Users.Any())
                return;
            
            userManager.AddNewUserAsync("alice", "Alice Smith",
                    "AliceSmith@email.com", "Pass123$", "20480835-FAA6-4495-8A7C-29E7CE175888").Wait();

            userManager.AddNewUserAsync("bob", "Bob Smith",
                "BobSmith@email.com", "Pass123$", "ca257d10-1615-4b8e-92d8-38366ae805b0").Wait();

            Log.Debug("test users created");

        }


        private static void EnsureSeedData(ConfigurationDbContext context)
        {
            if (!context.Clients.Any())
            {
                Log.Debug("Clients being populated");
                foreach (var client in Configuration.Clients.ToList())
                {
                    context.Clients.Add(client.ToEntity());
                }

                context.SaveChanges();
            }

            if (!context.IdentityResources.Any())
            {
                Log.Debug("IdentityResources being populated");
                foreach (var resource in Configuration.IdentityResources.ToList())
                {
                    context.IdentityResources.Add(resource.ToEntity());
                }

                context.SaveChanges();
            }

            if (!context.ApiScopes.Any())
            {
                Log.Debug("ApiScopes being populated");
                foreach (var resource in Configuration.ApiScopes.ToList())
                {
                    context.ApiScopes.Add(resource.ToEntity());
                }

                context.SaveChanges();
            }

            if (!context.ApiResources.Any())
            {
                Log.Debug("ApiResources being populated");
                foreach (var resource in Configuration.ApiResources.ToList())
                {
                    context.ApiResources.Add(resource.ToEntity());
                }

                context.SaveChanges();
            }
                       

        }

    }
}