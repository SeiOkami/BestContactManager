using System.ComponentModel.DataAnnotations;

namespace Contacts.WebClient.Models
{
    public class ContactModel
    {
        public Guid Id { get; set; }
        
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = "";

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Display(Name = "Middle Name")]
        public string? MiddleName { get; set; }
        public string? Phone { get; set; }

        [EmailAddress()]
        public string? Email { get; set; }

        public string? Description { get; set; }

    }
}
