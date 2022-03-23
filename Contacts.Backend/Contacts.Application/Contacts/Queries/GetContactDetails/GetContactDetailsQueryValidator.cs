using System;
using FluentValidation;

namespace Contacts.Application.Contacts.Queries.GetContactDetails
{
    public class GetContactDetailsQueryValidator : AbstractValidator<GetContactDetailsQuery>
    {
        public GetContactDetailsQueryValidator()
        {
            RuleFor(command => command.UserId).NotEqual(Guid.Empty);
            RuleFor(command => command.Id).NotEqual(Guid.Empty);
        }
    }
}
