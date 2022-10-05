using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfSandbox.Interfaces.Windows;
using WpfSandbox.ViewModels;

namespace WpfSandbox.Interfaces
{
    interface IMainWindowViewModelFactory
    {
        MainWindowViewModel Create(IWindow window);
    }
}
