using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Contacts.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Contacts.Application.Common.Exceptions;
using Contacts.Domain;

namespace Contacts.Application.Contacts.Queries.GetContactDetails
{
    public class GetContactDetailsQueryHandler 
        : IRequestHandler<GetContactDetailsQuery, ContactDetailsVm>
    {
        private readonly IContactsDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetContactDetailsQueryHandler(
            IContactsDbContext dbContext, IMapper mapper) 
            => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<ContactDetailsVm> Handle(
            GetContactDetailsQuery request, 
            CancellationToken cancellationToken)
        {
            var contact = await _dbContext.Contacts
                .FirstOrDefaultAsync(
                el => (el.UserId == request.UserId && el.Id == request.Id)
                , cancellationToken);

            if (contact == null)
                throw new NotFoundException(nameof(Contact), request.Id);

            return _mapper.Map<ContactDetailsVm>(contact);
        }
    }
}
