using Contacts.Application.Interfaces;
using Contacts.Application.Common.Exceptions;
using Contacts.Domain;
using MediatR;

namespace Contacts.Application.Contacts.Commands.ClearContacts
{
    public class ClearContactsCommandHandler
        : IRequestHandler<ClearContactsCommand>
    {

        private readonly IContactsDbContext _dbContext;

        public ClearContactsCommandHandler(IContactsDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(
            ClearContactsCommand request, CancellationToken cancellationToken)
        {
            var contacts = _dbContext.Contacts.Where(contact => contact.UserId == request.UserId);

            _dbContext.Contacts.RemoveRange(contacts);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

    }
}
