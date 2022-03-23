using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Contacts.Application.Interfaces;

namespace Contacts.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(
            this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<ContactsDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            services.AddScoped<IContactsDbContext>(
                provider => 
                {
                    var res = provider.GetService<ContactsDbContext>();
                    if (res == null) throw new Exception();
                    return res;
                }
            );

            return services;
        }
    }
}
