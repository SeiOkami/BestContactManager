using System;
using FluentValidation;

namespace Contacts.Application.Contacts.Queries.GetContactList
{
    public class GetContactListQueryValidator : AbstractValidator<GetContactListQuery>
    {
        public GetContactListQueryValidator()
        {
            RuleFor(command => command.UserId).NotEqual(Guid.Empty);
        }
    }
}
