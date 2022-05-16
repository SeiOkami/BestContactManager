using System;
using FluentValidation;

namespace Contacts.Application.Contacts.Commands.DeleteContact
{
    public class ClearContactsCommandValidator : AbstractValidator<ClearContactsCommand>
    {
        public ClearContactsCommandValidator()
        {
            RuleFor(command => command.UserId).NotEqual(Guid.Empty);
        }
    }
}
