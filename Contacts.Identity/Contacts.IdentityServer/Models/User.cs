using Microsoft.AspNetCore.Identity;

namespace Contacts.IdentityServer.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }
}
