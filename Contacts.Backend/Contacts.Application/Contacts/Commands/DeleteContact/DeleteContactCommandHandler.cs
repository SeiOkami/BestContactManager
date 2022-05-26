using Contacts.Application.Interfaces;
using Contacts.Application.Common.Exceptions;
using Contacts.Domain;
using MediatR;

namespace Contacts.Application.Contacts.Commands.DeleteContact
{
    public class DeleteContactCommandHandler 
        : IRequestHandler<DeleteContactCommand>
    {

        private readonly IContactsDbContext _dbContext;

        public DeleteContactCommandHandler(IContactsDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Unit> Handle(
            DeleteContactCommand request, CancellationToken cancellationToken)
        {
            
            var contact = await _dbContext.Contacts.FindAsync(
                new object[] { request.UserId, request.Id }
                , cancellationToken);

            if (contact == null)
                throw new NotFoundException(nameof(Contact), request.Id);

            _dbContext.Contacts.Remove(contact);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

    }
}
