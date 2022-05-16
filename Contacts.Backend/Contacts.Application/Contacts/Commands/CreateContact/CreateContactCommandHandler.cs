using Contacts.Domain;
using Contacts.Application.Interfaces;
using MediatR;

namespace Contacts.Application.Contacts.Commands.CreateContact
{
    public class CreateContactCommandHandler
        :IRequestHandler<CreateContactCommand, Guid>
    {

        private readonly IContactsDbContext _dbContext;
        public CreateContactCommandHandler(IContactsDbContext dbContext) =>
            _dbContext = dbContext;

        public async Task<Guid> Handle(
            CreateContactCommand request, CancellationToken cancellationToken)
        {

            var contact = new Contact
            {
                UserId = request.UserId,
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                Email = request.Email,
                Phone = request.Phone,
                Description = request.Description
            };

            await _dbContext.Contacts.AddAsync(contact, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return contact.Id;
        }
    }
}
