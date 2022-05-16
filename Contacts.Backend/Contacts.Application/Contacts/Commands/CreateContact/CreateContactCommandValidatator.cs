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
            RuleFor(CreateContactCommand =>
                CreateContactCommand.LastName).MaximumLength(250);
            RuleFor(CreateContactCommand =>
                CreateContactCommand.MiddleName).MaximumLength(250);
            RuleFor(CreateContactCommand =>
                CreateContactCommand.Phone).MaximumLength(100);
            RuleFor(CreateContactCommand =>
                CreateContactCommand.Email).EmailAddress();
        }
    }
}
