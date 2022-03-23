using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Contacts.Application.Common.Exceptions;
using Contacts.Application.Contacts.Commands.UpdateContact;
using Contacts.Tests.Common;
using Xunit;

namespace Contacts.Tests.Contacts.Commands
{
    public class UpdateContactCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateContactCommandHandler_Success()
        {
            // Arrange
            var handler   = new UpdateContactCommandHandler(Context);
            var firstName = "vasya pupkin";

            // Act
            await handler.Handle(new UpdateContactCommand
            {
                Id = ContactsContextFactory.ContactIdForUpdate,
                UserId = ContactsContextFactory.UserBId,
                FirstName = firstName
            }, CancellationToken.None);

            // Assert
            Assert.NotNull(await Context.Contacts.SingleOrDefaultAsync(Contact =>
                Contact.Id == ContactsContextFactory.ContactIdForUpdate &&
                Contact.FirstName == firstName));
        }

        [Fact]
        public async Task UpdateContactCommandHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new UpdateContactCommandHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new UpdateContactCommand
                    {
                        Id = Guid.NewGuid(),
                        UserId = ContactsContextFactory.UserAId
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task UpdateContactCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var handler = new UpdateContactCommandHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(
                    new UpdateContactCommand
                    {
                        Id = ContactsContextFactory.ContactIdForUpdate,
                        UserId = ContactsContextFactory.UserAId
                    },
                    CancellationToken.None);
            });
        }
    }
}