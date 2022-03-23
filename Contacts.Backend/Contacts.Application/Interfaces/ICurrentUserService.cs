using System;

namespace Contacts.Application.Interfaces
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
    }
}