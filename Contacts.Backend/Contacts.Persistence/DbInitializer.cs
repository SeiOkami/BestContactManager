
namespace Contacts.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(ContactsDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
