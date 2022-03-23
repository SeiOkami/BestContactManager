using MediatR;

namespace Contacts.Application.Contacts.Queries.GetContactList
{
    public class GetContactListQuery : IRequest<ContactListVm>
    {
        public Guid UserId { get; set; }
    }
}
