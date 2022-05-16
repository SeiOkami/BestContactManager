using Contacts.Application.Interfaces;
using Contacts.Application.Common.Exceptions;
using Contacts.Domain;
using MediatR;

namespace Contacts.Application.Contacts.Commands.GenerateContacts
{
    public class GenerateContactsCommandHandler
        : IRequestHandler<GenerateContactsCommand>
    {

        private readonly IContactsDbContext _dbContext;

        public GenerateContactsCommandHandler(IContactsDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(
            GenerateContactsCommand request, CancellationToken cancellationToken)
        {
            await _dbContext.GenerateAsync(request.UserId);

            return Unit.Value;
        }

    }
}
