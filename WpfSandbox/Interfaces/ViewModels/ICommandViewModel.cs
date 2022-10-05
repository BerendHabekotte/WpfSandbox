using System.Windows.Input;

namespace WpfSandbox.Interfaces.ViewModels
{
    public interface ICommandViewModel
    {
        ICommand Command { get;  }
    }
}
