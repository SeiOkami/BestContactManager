using MediatR;
using Microsoft.EntityFrameworkCore;
using Contacts.Application.Interfaces;
using Contacts.Application.Common.Exceptions;
using Contacts.Domain;

namespace Contacts.Application.Contacts.Commands.UpdateContact
{
    public class UpdateContactCommandHandler
        : IRequestHandler<UpdateContactCommand>
    {

        private readonly IContactsDbContext _dbContext;
        public UpdateContactCommandHandler(IContactsDbContext dbContext) =>
            _dbContext = dbContext;


        public async Task<Unit> Handle(
            UpdateContactCommand request, 
            CancellationToken cancellationToken)
        {
            
            var contact = await _dbContext.Contacts.FirstOrDefaultAsync(
                item => item.Id == request.Id, cancellationToken);
            
            if (contact == null || contact.UserId != request.UserId)
                throw new NotFoundException(nameof(Contact), request.Id);

            contact.FirstName = request.FirstName;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
