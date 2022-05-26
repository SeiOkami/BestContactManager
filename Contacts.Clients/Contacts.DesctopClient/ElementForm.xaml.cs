using Contacts.DesctopClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Contacts.DesctopClient
{
    
    public partial class ElementForm : Window
    {

        private ContactModel element;

        private bool isCorrect
        {
            get
            {
                return !String.IsNullOrWhiteSpace(element.FirstName);
            }
        }
        
        public ElementForm(ContactModel element)
        {
            InitializeComponent();

            this.element = element;
            DataContext = element;
        }
        
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (!isCorrect)
                MessageBox.Show($"There are empty fields!");
            else
                this.DialogResult = true;
        }
        
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        
        private void CloseExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
