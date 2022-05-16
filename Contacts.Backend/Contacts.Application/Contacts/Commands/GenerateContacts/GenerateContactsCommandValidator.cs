using System;
using FluentValidation;

namespace Contacts.Application.Contacts.Commands.GenerateContacts
{
    public class GenerateContactsCommandValidator : AbstractValidator<GenerateContactsCommand>
    {
        public GenerateContactsCommandValidator()
        {
            RuleFor(command => command.UserId).NotEqual(Guid.Empty);
        }
    }
}
