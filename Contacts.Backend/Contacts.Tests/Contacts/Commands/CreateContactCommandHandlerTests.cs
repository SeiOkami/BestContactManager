using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Contacts.Application.Contacts.Commands.CreateContact;
using Contacts.Tests.Common;
using Xunit;

namespace Contacts.Tests.Contacts.Commands
{
    public class CreateContactCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateContactCommandHandler_Success()
        {
            // Arrange
            var handler = new CreateContactCommandHandler(Context);
            var firstName = "anonimus";
            
            // Act
            var ContactId = await handler.Handle(
                new CreateContactCommand
                {
                    FirstName = firstName,
                    UserId = ContactsContextFactory.UserAId
                },
                CancellationToken.None);

            // Assert
            Assert.NotNull(
                await Context.Contacts.SingleOrDefaultAsync(Contact =>
                    Contact.Id == ContactId && Contact.FirstName == firstName
                    ));
        }
    }
}