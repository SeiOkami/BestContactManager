using Microsoft.EntityFrameworkCore;
using Contacts.Application.Interfaces;
using Contacts.Domain;
using Contacts.Persistence.EntityTypeConfigurations;
using RandomizerLibrary;
using Newtonsoft.Json;

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

        public async Task GenerateAsync(Guid UserID)
        {
            var minContactsCount = 100;
            var maxContactsCount = 500;
            var minLenghtEmail = 5;
            var maxLenghtEmail = 10;

            var random = new Random();

            var contactsCount = random.Next(minContactsCount, maxContactsCount);
            for (int i = 0; i < contactsCount; i++)
            {
                var FullName = Randomizer.RandomFullName(true).Split(" "); 
                
                var contact = new Contact();
                contact.Id = Guid.NewGuid();
                contact.UserId = UserID;
                contact.FirstName = FullName[0];
                contact.LastName = FullName[1];
                contact.MiddleName = FullName[2];
                contact.Phone = Randomizer.RandomPhone();
                contact.Email = Randomizer.RandomMail(minLenghtEmail, maxLenghtEmail);

                Contacts.Add(contact);
            }

            await SaveChangesAsync();
        }

    }
}
