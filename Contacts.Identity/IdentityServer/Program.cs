using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using IdentityServer.Models;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlite(connectionString));builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<ApplicationContext>();
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

//services.Configure<IdentityOptions>(builder.Configuration.GetSection(nameof(IdentityOptions)));


builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Password.RequiredLength = 4;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders().AddDefaultUI();

//services.AddApiAuthorization<User, ApplicationContext>();

services.Configure<IdentityOptions>(builder.Configuration.GetSection(nameof(IdentityOptions)));


//builder.Services.Configure<IdentityOptions>(options =>
//{
//    options.Password.RequiredLength = 4;
//    options.Password.RequireDigit = false;
//    options.Password.RequireNonAlphanumeric = false;
//    options.Password.RequireUppercase = false;
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
app.MapRazorPages();

app.Run();
