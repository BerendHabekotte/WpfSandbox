using System;
using System.Windows.Input;

namespace BcWpfCommon.Commands
{
    public class ActionCommand<T> : ICommand
    {
        private readonly Action<T> execute;

        private readonly Predicate<object> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public ActionCommand(Action<T> execute) : this(execute, null)
        { }

        public ActionCommand(Action<T> execute, Predicate<object> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException("execute");
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute((T)parameter);
        }
    }
}
