using System;
using Microsoft.EntityFrameworkCore;
using Contacts.Domain;
using Contacts.Persistence;

namespace Contacts.Tests.Common
{
    public class ContactsContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();

        public static Guid ContactIdForDelete = Guid.NewGuid();
        public static Guid ContactIdForUpdate = Guid.NewGuid();

        public static ContactsDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ContactsDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new ContactsDbContext(options);
            context.Database.EnsureCreated();
            context.Contacts.AddRange(
                new Contact
                {
                    Id = Guid.Parse("7D6286FA-9706-4523-B395-97E51B75C75A"),
                    UserId = UserAId,
                    FirstName = "FirstName1",
                    LastName = "LastName1"
                },
                new Contact
                {
                    Id = Guid.Parse("0CB6B724-B37B-4CB3-9121-FBC49B6DF387"),
                    UserId = UserBId,
                    FirstName = "FirstName2",
                    LastName = "LastName2"
                },
                new Contact
                {
                    Id = ContactIdForDelete,
                    UserId = UserAId,
                    FirstName = "FirstName3",
                    LastName = "LastName3"
                },
                new Contact
                {
                    Id = ContactIdForUpdate,
                    UserId = UserBId,
                    FirstName = "FirstName4",
                    LastName = "LastName4"
                }
            );
            context.SaveChanges();
            return context;
        }

        public static void Destroy(ContactsDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
