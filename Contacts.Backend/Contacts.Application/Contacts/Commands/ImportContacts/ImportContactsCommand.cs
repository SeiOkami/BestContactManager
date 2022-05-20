using Contacts.Application.Interfaces;
using Contacts.Application.Common.Exceptions;
using Contacts.Domain;
using MediatR;
using Contacts.Application.Contacts.Commands.UpdateContact;

namespace Contacts.Application.Contacts.Commands.ImportContacts
{
    public class ImportContactsCommand : IRequest
    {
        public Guid UserId { get; set; }
        public List<UpdateContactCommand> Contacts { get; set; } = null!;
        public IMediator Mediator { get; set; } = null!;
    }
}
