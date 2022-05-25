using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Contacts.DesctopClient.ViewModels;

namespace Contacts.DesctopClient.Commands
{
    public class UpdateCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;//; throw new NotImplementedException();
        }

        public void Execute(object? parameter)
        {
            var appView = (ApplicationViewModel)parameter;
        }
    }
}
