using System;
using FluentValidation;

namespace Contacts.Application.Contacts.Commands.CreateContact
{
    public class CreateContactCommandValidatator : AbstractValidator<CreateContactCommand>
    {
        public CreateContactCommandValidatator()
        {
            RuleFor(CreateContactCommand =>
                CreateContactCommand.FirstName).NotEmpty().MaximumLength(250);
            RuleFor(CreateContactCommand =>
                CreateContactCommand.UserId).NotEqual(Guid.Empty);
        }
    }
}
