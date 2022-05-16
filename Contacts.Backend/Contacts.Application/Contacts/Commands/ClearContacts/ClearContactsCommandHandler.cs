﻿using Contacts.Application.Interfaces;
using Contacts.Application.Common.Exceptions;
using Contacts.Domain;
using MediatR;

namespace Contacts.Application.Contacts.Commands.DeleteContact
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
            
            var contact = await _dbContext.Contacts.FindAsync(
                new object[] { request.Id }, cancellationToken);

            if (contact == null || contact.UserId != request.UserId)
                throw new NotFoundException(nameof(Contact), request.Id);

            _dbContext.Contacts.Remove(contact);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

    }
}
