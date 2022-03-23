using System.Collections.Generic;

namespace Contacts.Application.Contacts.Queries.GetContactList
{
    public class ContactListVm
    {
        public IList<ContactLookupDto> Contacts { get; set; } = null!;
    }
}
