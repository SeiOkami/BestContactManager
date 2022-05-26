using System;
using System.Windows;
using System.Windows.Input;

namespace Contacts.DesctopClient.ViewModels
{
    public class RelayCommand : ICommand
    {
        public Action? ExecuteMethod;
        public Action<object?>? ExecuteMethodParam;

        public Func<bool>? CanExecuteMethod;
        public Func<object?, bool>? CanExecuteMethodParam;

        public string? TextQuestion;

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public RelayCommand(Action<object?> executeMethodParam,
            Func<object?, bool>? canExecuteMethodParam = null, string? textQuetion = null)
        {
            ExecuteMethodParam = executeMethodParam;
            CanExecuteMethodParam = canExecuteMethodParam;
            TextQuestion = textQuetion;
        }
        public RelayCommand(Action executeMethod, Func<bool>? canExecuteMethod = null, string? textQuetion = null)
        {
            ExecuteMethod = executeMethod;
            CanExecuteMethod = canExecuteMethod;
            TextQuestion = textQuetion;
        }

        public bool CanExecute(object? parameter)
        {
            if (CanExecuteMethodParam != null)
                return CanExecuteMethodParam(parameter);
            else if (CanExecuteMethod != null)
                return CanExecuteMethod();
            else
                return true;
        }

        public void Execute(object? parameter)
        {
            if (!string.IsNullOrEmpty(TextQuestion))
            {
                var answer = MessageBox.Show(TextQuestion, "",
                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (answer != MessageBoxResult.Yes)
                    return;
            }

            if (ExecuteMethodParam != null)
                ExecuteMethodParam(parameter);
            else if (ExecuteMethod != null)
                ExecuteMethod();
        }
    }
}
