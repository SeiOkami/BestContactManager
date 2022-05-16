using System;
using MediatR;

namespace Contacts.Application.Contacts.Commands.UpdateContact
{
    public class UpdateContactCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string FirstName { get; set; } = "";

        public string? LastName { get; set; } = "";

        public string? MiddleName { get; set; } = "";

        public string? Phone { get; set; } = "";

        public string? Email { get; set; } = "";

        public string? Description { get; set; } = "";
    }
}
