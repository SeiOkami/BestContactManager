using System;
using FluentValidation;

namespace Contacts.Application.Contacts.Commands.UpdateContact
{
    public class UpdateContactCommandValidator : AbstractValidator<UpdateContactCommand>
    {
        public UpdateContactCommandValidator()
        {
            RuleFor(command => command.UserId).NotEqual(Guid.Empty);
            RuleFor(command => command.Id).NotEqual(Guid.Empty);
            RuleFor(command => command.FirstName).NotEmpty().MaximumLength(250);
        }
    }
}
