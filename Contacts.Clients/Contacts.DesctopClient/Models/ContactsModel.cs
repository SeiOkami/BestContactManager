using System.Collections;
using System.Collections.ObjectModel;

namespace Contacts.DesctopClient.Models
{
    public class ContactsModel
    {
        public ObservableCollection<ContactModel> Contacts { get; set; } = new();        
    }
}
