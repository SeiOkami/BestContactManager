using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Contacts.Domain;

namespace Contacts.Persistence.EntityTypeConfigurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(contact => new { contact.UserId, contact.Id });
            builder.HasIndex(contact => new { contact.UserId, contact.Id }).IsUnique();
            builder.Property(contact => contact.FirstName).HasMaxLength(100);
            builder.Property(contact => contact.LastName).HasMaxLength(100);
            builder.Property(contact => contact.MiddleName).HasMaxLength(100);
        }
    }
}
