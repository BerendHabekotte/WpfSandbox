using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSandbox.ViewModels;

namespace WpfSandbox.Interfaces.ViewModels
{
    public interface IMainWindowViewModel
    {
        ReadOnlyCollection<ICommandViewModel> Commands { get; }

        ObservableCollection<WorkspaceViewModel> Workspaces { get; }
    }
}
