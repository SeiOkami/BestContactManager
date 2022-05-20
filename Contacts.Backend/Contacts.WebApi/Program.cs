using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Reflection;
using Serilog;
using Serilog.Events;
using Contacts.Persistence;
using Contacts.Application;
using Contacts.Application.Interfaces;
using Contacts.Application.Common.Mappings;
using Contacts.WebApi.Middleware;
using Contacts.WebApi.Services;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Contacts.WebApi

{
    public class Program
    {

        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog();

            var services = builder.Services;


            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(IContactsDbContext).Assembly));
            });

            services.AddApplication();
            services.AddPersistence(builder.Configuration);
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });

            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://localhost:7251";
                options.Audience = "ContactsWebAPI";
                options.RequireHttpsMetadata = false;
            });

            services.AddVersionedApiExplorer(opt => opt.GroupNameFormat = "'v'VVV");
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddSwaggerGen(config =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
            });

            services.AddApiVersioning();

            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .WriteTo.File(@"Logs\Log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var app = builder.Build();

            app.UseSwagger();


            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var context = serviceProvider.GetRequiredService<ContactsDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    Log.Fatal(ex, "An error occurred while app initialization");
                }

                var apiProvider = serviceProvider.GetRequiredService<IApiVersionDescriptionProvider>();
                app.UseSwaggerUI(config =>
                {
                    if (apiProvider != null)
                    {
                        foreach (var description in apiProvider.ApiVersionDescriptions)
                        {
                            config.SwaggerEndpoint(
                                $"/swagger/{description.GroupName}/swagger.json",
                                description.GroupName.ToUpperInvariant());
                            config.RoutePrefix = string.Empty;
                        }
                    }
                });
            }

            app.UseCustomExceptionHandler();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseApiVersioning();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }

}