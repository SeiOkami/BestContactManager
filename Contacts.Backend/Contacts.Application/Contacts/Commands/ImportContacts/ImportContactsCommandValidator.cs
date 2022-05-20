using System;
using FluentValidation;

namespace Contacts.Application.Contacts.Commands.ImportContacts
{
    public class ImportContactsCommandValidator : AbstractValidator<ImportContactsCommand>
    {
        public ImportContactsCommandValidator()
        {
            RuleFor(command => command.UserId).NotEqual(Guid.Empty);
            RuleFor(command => command.Contacts).NotEmpty();
        }
    }
}
