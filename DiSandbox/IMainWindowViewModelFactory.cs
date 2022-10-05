using DiPresentationLogic;

namespace DiSandbox
{
    public interface IMainWindowViewModelFactory
    {
        MainWindowViewModel Create(IWindow window);
    }
}
