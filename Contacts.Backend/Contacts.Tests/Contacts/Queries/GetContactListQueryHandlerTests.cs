using AutoMapper;
using System.Threading;
using System.Threading.Tasks;
using Contacts.Application.Contacts.Queries.GetContactList;
using Contacts.Persistence;
using Contacts.Tests.Common;
using Shouldly;
using Xunit;

namespace Contacts.Tests.Contacts.Queries
{
    [Collection("QueryCollection")]
    public class GetContactListQueryHandlerTests
    {
        private readonly ContactsDbContext Context;
        private readonly IMapper Mapper;

        public GetContactListQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetContactListQueryHandler_Success()
        {
            // Arrange
            var handler = new GetContactListQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetContactListQuery
                {
                    UserId = ContactsContextFactory.UserBId
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<ContactListVm>();
            result.Contacts.Count.ShouldBe(2);
        }
    }
}