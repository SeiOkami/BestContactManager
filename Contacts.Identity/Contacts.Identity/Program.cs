using System.IO;
using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using IdentityModel;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Contacts.Identity;
using Contacts.Identity.Data;
using Contacts.Identity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;
using IdentityServer4.Services;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.ResponseHandling;
using Microsoft.Extensions.DependencyInjection.Extensions;
using IdentityServer4.AspNetIdentity;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetValue<string>("DbConnection");
var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

var services = builder.Services;


services.AddDbContext<AuthDbContext>(options =>
{
    options.UseSqlite(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly));
});


services.AddIdentity<AppUser, IdentityRole>(config =>
{
    config.Password.RequiredLength = 4;
    config.Password.RequireDigit = false;
    config.Password.RequireNonAlphanumeric = false;
    config.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<AuthDbContext>().AddDefaultTokenProviders();


services.AddIdentityServer()
    .AddAspNetIdentity<AppUser>().AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = builder => builder.UseSqlite(connectionString,
           opt => opt.MigrationsAssembly(migrationsAssembly));
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = builder => builder.UseSqlite(connectionString,
            opt => opt.MigrationsAssembly(migrationsAssembly));

        options.EnableTokenCleanup = true;
    })
    .AddDeveloperSigningCredential();

services.AddControllersWithViews();



var app = builder.Build();

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Code)
    .CreateLogger();

Log.Information("Starting host...");

DbInitializer.Initialize(app.Services);

app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());



app.Run();
