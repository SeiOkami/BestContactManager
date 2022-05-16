using MediatR;

namespace Contacts.Application.Contacts.Commands.ClearContacts
{
    public class ClearContactsCommand : IRequest
    {
        public Guid UserId { get; set; }
    }
}
