using System;
using System.Windows.Input;

namespace ExchangeOfCurrencies.Commands
{
    internal class ParameterlessCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool>? _canExecute;


        public ParameterlessCommand(Action execute, Func<bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }


        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public void Execute(object? parameter)
        {
            _execute();
        }
    }
}