using System;
using System.Windows.Input;
using WpfSandbox.Interfaces.ViewModels;

namespace WpfSandbox.ViewModels
{
    public class CommandViewModel : ViewModelBase, ICommandViewModel
    {
        public ICommand Command { get; private set; }

        public CommandViewModel(string displayName, ICommand command)
        {
            Command = command ?? throw new ArgumentNullException("command");
            DisplayName = displayName;
        }
    }
}
