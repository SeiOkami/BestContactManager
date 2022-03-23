using System;
using Contacts.Persistence;

namespace Contacts.Tests.Common
{
    public abstract class TestCommandBase : IDisposable
    {
        protected readonly ContactsDbContext Context;

        public TestCommandBase()
        {
            Context = ContactsContextFactory.Create();
        }

        public void Dispose()
        {
            ContactsContextFactory.Destroy(Context);
        }
    }
}