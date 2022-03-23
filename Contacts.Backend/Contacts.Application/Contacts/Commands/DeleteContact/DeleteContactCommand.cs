using MediatR;

namespace Contacts.Application.Contacts.Commands.DeleteContact
{
    public class DeleteContactCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}
