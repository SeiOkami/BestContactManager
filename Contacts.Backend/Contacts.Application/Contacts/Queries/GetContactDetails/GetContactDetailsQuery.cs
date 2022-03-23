using System;
using MediatR;

namespace Contacts.Application.Contacts.Queries.GetContactDetails
{
    public class GetContactDetailsQuery : IRequest<ContactDetailsVm>
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}
