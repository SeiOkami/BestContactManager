using System.ComponentModel.DataAnnotations;

namespace Contacts.WebClient.Models
{
    public class GenerateContactsModel
    {
        [Display(Name = "Clear database?")]
        public bool Clear { get; set; }
    }
}
