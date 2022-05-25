using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.DesctopClient.Models
{
    public class ContactModel : INotifyPropertyChanged
    {

        private Guid id;
        private string firstName = String.Empty;
        private string? lastName;
        private string? middleName;
        private string? phone;
        private string? email;
        private string? description;



        public Guid Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                OnPropertyChanged();
            }
        }

        public string? LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                OnPropertyChanged();
            }
        }

        public string? MiddleName
        {
            get { return middleName; }
            set
            {
                middleName = value;
                OnPropertyChanged();
            }
        }

        public string? Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                OnPropertyChanged();
            }
        }

        public string? Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        public string? Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }

        public void Fill(ContactModel contact)
        {
            this.FirstName = contact.FirstName;
            this.LastName = contact.LastName;
            this.MiddleName = contact.MiddleName;
            this.Phone = contact.Phone;
            this.Email = contact.Email;
            this.Description = contact.Description;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
