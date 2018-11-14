using System;
using System.Windows.Input;

namespace ViewModel.Commands
{
    class RelayCommand : ICommand
    {
        readonly Action _execute;
        readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
            {
                throw new NullReferenceException("execute");
            }
            _execute = execute;
            _canExecute = canExecute;
        }
        public RelayCommand(Action execute) : this(execute, null)
        {

        }
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            if (this._canExecute == null)
                return true;
            if (parameter == null)
                return this._canExecute();
            return this._canExecute();
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}
