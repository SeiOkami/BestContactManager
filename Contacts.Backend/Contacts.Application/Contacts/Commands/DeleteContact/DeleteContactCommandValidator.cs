using System;
using FluentValidation;

namespace Contacts.Application.Contacts.Commands.DeleteContact
{
    public class DeleteContactCommandValidator : AbstractValidator<DeleteContactCommand>
    {
        public DeleteContactCommandValidator()
        {
            RuleFor(command => command.UserId).NotEqual(Guid.Empty);
            RuleFor(command => command.Id).NotEqual(Guid.Empty);
        }
    }
}
