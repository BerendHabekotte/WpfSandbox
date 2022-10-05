namespace WpfSandbox.Interfaces.Windows
{
    public interface IWindow
    {
        void Close();

        IWindow CreateChild(object viewModel);

        void Show();

        bool? ShowDialog();
    }
}
