using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Contacts.Application.Interfaces;

namespace Contacts.Application.Contacts.Queries.GetContactList
{
    public class GetContactListQueryHandler 
        : IRequestHandler<GetContactListQuery, ContactListVm>
    {
        private readonly IContactsDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetContactListQueryHandler(
            IContactsDbContext dbContext, IMapper mapper) 
            => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<ContactListVm> Handle(GetContactListQuery request,
            CancellationToken cancellationToken)
        {
            var contactsQuery = await _dbContext.Contacts
                .Where(contact => contact.UserId == request.UserId)
                .ProjectTo<ContactLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new ContactListVm { Contacts = contactsQuery };
        }
    }
}
