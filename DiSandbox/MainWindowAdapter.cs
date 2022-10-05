using DiPresentationLogic;
using System;
using System.Windows;
using System.Windows.Input;

namespace DiSandbox
{
    public class MainWindowAdapter : WindowAdapter
    {
        private readonly IMainWindowViewModelFactory viewModelFactory;
        private bool initialized;

        public MainWindowAdapter(Window wpfWindow, IMainWindowViewModelFactory viewModelFactory) 
            : base(wpfWindow)
        { 
            if (viewModelFactory == null)
            {
                throw new ArgumentNullException("viewModelFactory");
            }
            this.viewModelFactory = viewModelFactory;
        }

        public override void Close()
        {
            EnsureInitialized();
            base.Close();
        }

        public override IWindow CreateChild(object viewModel)
        {
            EnsureInitialized();
            return base.CreateChild(viewModel);
        }

        public override void Show()
        {
            EnsureInitialized();
            base.Show();
        }

        public override bool? ShowDialog()
        {
            EnsureInitialized();
            return base.ShowDialog();
        }

        private void DeclareKeyBindings(MainWindowViewModel vm)
        {
            WpfWindow.InputBindings.Add(new KeyBinding(vm.RefreshCommand, new KeyGesture(Key.F5)));
            WpfWindow.InputBindings.Add(new KeyBinding(vm.InsertCustomerCommand, new KeyGesture(Key.Insert)));
            WpfWindow.InputBindings.Add(new KeyBinding(vm.EditCustomerCommand, new KeyGesture(Key.Enter)));
            WpfWindow.InputBindings.Add(new KeyBinding(vm.DeleteCustomerCommand, new KeyGesture(Key.Delete)));
        }

        private void EnsureInitialized()
        {
            if (initialized)
            {
                return;
            }
            var viewModel = viewModelFactory.Create(this);
            WpfWindow.DataContext = viewModel;
            DeclareKeyBindings(viewModel);
            viewModel.RefreshCommand.Execute(null);
            initialized = true;
        }
    }
}
