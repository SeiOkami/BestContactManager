using MediatR;

namespace Contacts.Application.Contacts.Commands.GenerateContacts
{
    public class GenerateContactsCommand : IRequest
    {
        public Guid UserId { get; set; }
    }
}
