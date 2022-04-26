using System.IO;
using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityModel;
using Contacts.Identity;
using Contacts.Identity.Data;
using Contacts.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetValue<string>("DbConnection");

builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseSqlite(connectionString);
});

builder.Services.AddIdentity<AppUser, IdentityRole>(config =>
{
    config.Password.RequiredLength = 4;
    config.Password.RequireDigit = false;
    config.Password.RequireNonAlphanumeric = false;
    config.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<AuthDbContext>().AddDefaultTokenProviders();

builder.Services.AddIdentityServer()
    .AddAspNetIdentity<AppUser>()
    .AddInMemoryApiResources(Configuration.ApiResources)
    .AddInMemoryIdentityResources(Configuration.IdentityResources)
    .AddInMemoryApiScopes(Configuration.ApiScopes)
    .AddInMemoryClients(Configuration.Clients)
    .AddTestUsers(Configuration.TestUsers)
    .AddDeveloperSigningCredential();

builder.Services.AddControllersWithViews();

builder.Services.ConfigureApplicationCookie(config =>
{
    config.Cookie.Name = "Contacts.Identity.Cookie";
    config.LoginPath = "/Auth/Login";
    config.LogoutPath = "/Auth/Logout";
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<AuthDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while app initialization");
    }
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
                    Path.Combine(app.Environment.ContentRootPath, "Styles")),
    RequestPath = "/styles"
});

app.UseRouting();
app.UseIdentityServer();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.MapGet("/", () => @"<a href='/Auth/Login'>login</a>");

app.Run();
