namespace DiPresentationLogic
{
    public interface IWindow
    {
        void Close();

        IWindow CreateChild(object ViewModel);

        void Show();

        bool? ShowDialog();
    }
}
