using Microsoft.EntityFrameworkCore;
using Contacts.Application.Interfaces;
using Contacts.Domain;
using Contacts.Persistence.EntityTypeConfigurations;


namespace Contacts.Persistence
{
    public class ContactsDbContext : DbContext, IContactsDbContext
    {
        public DbSet<Contact> Contacts { get; set; } = null!;

        public ContactsDbContext(DbContextOptions<ContactsDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ContactConfiguration());
            base.OnModelCreating(builder);
        }

    }
}
