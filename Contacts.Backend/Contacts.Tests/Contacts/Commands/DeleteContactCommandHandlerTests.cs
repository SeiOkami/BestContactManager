using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contacts.Application.Common.Exceptions;
using Contacts.Application.Contacts.Commands.DeleteContact;
using Contacts.Application.Contacts.Commands.CreateContact;
using Contacts.Tests.Common;
using Xunit;

namespace Contacts.Tests.Contacts.Commands
{
    public class DeleteContactCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteContactCommandHandler_Success()
        {
            // Arrange
            var handler = new DeleteContactCommandHandler(Context);

            // Act
            await handler.Handle(new DeleteContactCommand
            {
                Id = ContactsContextFactory.ContactIdForDelete,
                UserId = ContactsContextFactory.UserAId
            }, CancellationToken.None);

            // Assert
            Assert.Null(Context.Contacts.SingleOrDefault(Contact =>
                Contact.Id == ContactsContextFactory.ContactIdForDelete));
        }

        [Fact]
        public async Task DeleteContactCommandHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new DeleteContactCommandHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new DeleteContactCommand
                    {
                        Id = Guid.NewGuid(),
                        UserId = ContactsContextFactory.UserAId
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task DeleteContactCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var deleteHandler = new DeleteContactCommandHandler(Context);
            var createHandler = new CreateContactCommandHandler(Context);
            var ContactId = await createHandler.Handle(
                new CreateContactCommand
                {
                    FirstName = "anonimus",
                    UserId = ContactsContextFactory.UserAId
                }, CancellationToken.None);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await deleteHandler.Handle(
                    new DeleteContactCommand
                    {
                        Id = ContactId,
                        UserId = ContactsContextFactory.UserBId
                    }, CancellationToken.None));
        }
    }
}