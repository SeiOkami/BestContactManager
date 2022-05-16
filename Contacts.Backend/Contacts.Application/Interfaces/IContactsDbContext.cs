using Microsoft.EntityFrameworkCore;
using Contacts.Domain;

namespace Contacts.Application.Interfaces
{
    public interface IContactsDbContext
    {
        DbSet<Contact> Contacts { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task GenerateAsync(Guid UserID);

    }
}
