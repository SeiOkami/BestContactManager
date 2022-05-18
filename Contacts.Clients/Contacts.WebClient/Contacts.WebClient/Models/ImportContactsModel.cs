using System.ComponentModel.DataAnnotations;

namespace Contacts.WebClient.Models
{
    public class ImportContactsModel
    {
        [Display(Name = "File contacts")]
        public IFormFile FileContacts { get; set; } = null!;
        
        [Display(Name = "Clear database?")]
        public bool Clear { get; set; }
    }
}
