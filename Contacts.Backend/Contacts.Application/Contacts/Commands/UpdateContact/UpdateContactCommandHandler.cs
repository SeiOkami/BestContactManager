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
                item => item.Id == request.Id && item.UserId == request.UserId, cancellationToken);

            if (contact == null)
            {
                contact = new();
                contact.Id = request.Id;
                contact.UserId = request.UserId;
                await _dbContext.Contacts.AddAsync(contact);
            }

            contact.FirstName = request.FirstName;
            contact.LastName = request.LastName;
            contact.MiddleName = request.MiddleName;
            contact.Email = request.Email;
            contact.Phone = request.Phone;
            contact.Description = request.Description;
            
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
