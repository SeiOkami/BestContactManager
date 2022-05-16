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
            RuleFor(command => command.LastName).MaximumLength(250);
            RuleFor(command => command.MiddleName).MaximumLength(250);
            RuleFor(command => command.Phone).MaximumLength(100);
            RuleFor(command => command.Email).EmailAddress();

        }
    }
}
