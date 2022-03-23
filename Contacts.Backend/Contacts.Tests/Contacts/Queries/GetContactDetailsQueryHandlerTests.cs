using AutoMapper;
using System;
using System.Threading;
using System.Threading.Tasks;
using Contacts.Application.Contacts.Queries.GetContactDetails;
using Contacts.Persistence;
using Contacts.Tests.Common;
using Shouldly;
using Xunit;

namespace Contacts.Tests.Contacts.Queries
{
    [Collection("QueryCollection")]
    public class GetContactDetailsQueryHandlerTests
    {
        private readonly ContactsDbContext Context;
        private readonly IMapper Mapper;

        public GetContactDetailsQueryHandlerTests(QueryTestFixture fixture)
        {
            Context = fixture.Context;
            Mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetContactDetailsQueryHandler_Success()
        {
            // Arrange
            var handler = new GetContactDetailsQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetContactDetailsQuery
                {
                    UserId = ContactsContextFactory.UserBId,
                    Id = Guid.Parse("0CB6B724-B37B-4CB3-9121-FBC49B6DF387")
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<ContactDetailsVm>();
            result.FirstName.ShouldBe("FirstName2");
            result.LastName.ShouldBe("LastName2");
        }
    }
}