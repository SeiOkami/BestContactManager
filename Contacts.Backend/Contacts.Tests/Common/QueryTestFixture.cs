using AutoMapper;
using System;
using Contacts.Application.Interfaces;
using Contacts.Application.Common.Mappings;
using Contacts.Persistence;
using Xunit;

namespace Contacts.Tests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public ContactsDbContext Context;
        public IMapper Mapper;

        public QueryTestFixture()
        {
            Context = ContactsContextFactory.Create();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AssemblyMappingProfile(
                    typeof(IContactsDbContext).Assembly));
            });
            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            ContactsContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}