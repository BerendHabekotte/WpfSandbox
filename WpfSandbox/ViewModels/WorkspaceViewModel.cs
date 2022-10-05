using System;
using System.Windows.Input;
using WpfSandbox.Commands;

namespace WpfSandbox.ViewModels
{
    public abstract class WorkspaceViewModel : ViewModelBase
    {
        public event EventHandler RequestClose;

        public ICommand CloseCommand { get; set; }

        public WorkspaceViewModel()
        {
            CloseCommand = new RelayCommand(ParamArrayAttribute => OnRequestClose());
        }

        protected virtual void OnRequestClose()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

    }
}
