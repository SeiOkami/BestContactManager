using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Contacts.Application.Contacts.Commands.CreateContact
{
    public class CreateContactCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string FirstName { get; set; } = "";
        
    }
}
