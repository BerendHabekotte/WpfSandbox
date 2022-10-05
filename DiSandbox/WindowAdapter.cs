using DiPresentationLogic;
using System;
using System.Windows;
using System.Windows.Input;

namespace DiSandbox
{
    public class WindowAdapter : IWindow
    {
        private readonly Window wpfWindow;

        public WindowAdapter(Window wpfWindow)
        {
            if (wpfWindow == null)
            {
                throw new ArgumentNullException("wpfWindow");
            }
            this.wpfWindow = wpfWindow;
        }

        public virtual void Close()
        {
            wpfWindow.Close();
        }

        public virtual IWindow CreateChild(object viewModel)
        {
            var contentWindow = new ContentWindow();
            contentWindow.Owner = wpfWindow;
            contentWindow.DataContext = viewModel;
            ConfigureBehavior(contentWindow);
            return new WindowAdapter(contentWindow);
        }

        public virtual void Show()
        {
            wpfWindow.Show();
        }

        public virtual bool? ShowDialog()
        {
            return wpfWindow.ShowDialog();
        }

        protected Window WpfWindow => wpfWindow;

        private static void ConfigureBehavior(ContentWindow contentWindow)
        {
            contentWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            contentWindow.CommandBindings.Add(
                new CommandBinding(
                    PresentationCommands.Accept, 
                    (sender, e) => contentWindow.DialogResult = true));
        }
    }
}
