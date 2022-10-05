using System.Windows.Input;

namespace DiPresentationLogic
{
    public static class PresentationCommands
    {
        private readonly static RoutedCommand accept = new RoutedCommand("Accept", typeof(PresentationCommands));

        public static RoutedCommand Accept
        {
            get { return accept;  }
        }
    }
}
