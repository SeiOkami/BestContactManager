using Contacts.WebClient.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var Configuration = builder.Configuration;

// Add services to the container.
services.AddControllersWithViews();

//JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc", options =>
    {

        options.Authority = Configuration["InteractiveServiceSettings:AuthorityUrl"];
        options.ClientId = Configuration["InteractiveServiceSettings:ClientId"];
        options.ClientSecret = Configuration["InteractiveServiceSettings:ClientSecret"];

        options.ResponseType = "code";
        options.UsePkce = true;
        options.ResponseMode = "query";

        options.Scope.Add(Configuration["InteractiveServiceSettings:Scopes:0"]);
        options.SaveTokens = true;
    });

services.Configure<IdentityServerSettings>(Configuration.GetSection("IdentityServerSettings"));
services.AddSingleton<ITokenService, TokenService>();
//services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//.AddCookie();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
