using DiPresentationLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiSandbox
{
    public class MainWindowViewModelFactory : IMainWindowViewModelFactory
    {
        private readonly ICustomerManagementAgent agent;

        public MainWindowViewModelFactory(ICustomerManagementAgent agent)
        {
            if (agent == null)
            {
                throw new ArgumentNullException("agent");
            }
            this.agent = agent;
        }

        public MainWindowViewModel Create(IWindow window)
        {
            if (window == null)
            {
                throw new ArgumentNullException("window");
            }
            return new MainWindowViewModel(agent, window);
        }
    }
}
