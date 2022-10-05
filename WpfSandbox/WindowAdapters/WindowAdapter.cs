using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfSandbox.Interfaces.Windows;

namespace WpfSandbox.WindowAdapters
{
    public class WindowAdapter : IWindow
    {
        public WindowAdapter(Window wpfWindow)
        {
            this.wpfWindow = wpfWindow ?? throw new ArgumentNullException("wpfWindow");
        }

        public void Close()
        {
            wpfWindow.Close();
        }

        public IWindow CreateChild(object viewModel)
        {
            throw new NotImplementedException();
        }

        public void Show()
        {
            throw new NotImplementedException();
        }

        public bool? ShowDialog()
        {
            throw new NotImplementedException();
        }

        private readonly Window wpfWindow;
    }
}
