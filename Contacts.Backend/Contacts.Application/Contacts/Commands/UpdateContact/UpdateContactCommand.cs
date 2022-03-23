using System;
using MediatR;

namespace Contacts.Application.Contacts.Commands.UpdateContact
{
    public class UpdateContactCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string FirstName { get; set; } = "";
    }
}
