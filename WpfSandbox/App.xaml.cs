using System;
using System.Windows;
using WpfSandbox.ViewModels;

namespace WpfSandbox
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var window = new MainWindow();
            var path = "Data/customers.xml";
            var viewModel = new MainWindowViewModel(path);

            EventHandler handler = null;
            handler = delegate
            {
                viewModel.RequestClose -= handler;
                window.Close();
            };
            viewModel.RequestClose += handler;

            // Allow all controls in the window to bind to the ViewModel by setting the DataContext, wich propagates down the element tree.
            window.DataContext = viewModel;
            window.Show();
        }
    }
}
