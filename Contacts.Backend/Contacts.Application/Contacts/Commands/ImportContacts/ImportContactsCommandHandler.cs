using Contacts.Application.Interfaces;
using Contacts.Application.Common.Exceptions;
using Contacts.Domain;
using MediatR;

namespace Contacts.Application.Contacts.Commands.ImportContacts
{
    public class ImportContactsCommandHandler : IRequestHandler<ImportContactsCommand>
    {
        public async Task<Unit> Handle(
            ImportContactsCommand request, CancellationToken cancellationToken)
        {
            foreach (var updateCommand in request.Contacts)
            {
                updateCommand.UserId = request.UserId;
                await request.Mediator.Send(updateCommand);
            }

            return Unit.Value;
        }
    }
}
