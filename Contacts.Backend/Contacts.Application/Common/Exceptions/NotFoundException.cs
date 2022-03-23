
namespace Contacts.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base($"Element \"{name}\" ({key}) not found.") { }
    }
}
