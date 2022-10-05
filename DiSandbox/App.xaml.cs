using Autofac;
using DiPresentationLogic;
using System.Reflection;
using System.Windows;

namespace DiSandbox
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            RegisterApplication();
            container.Resolve<IWindow>().Show();

        }
        private static ContainerBuilder builder;
        private static IContainer container;
        public static ContainerBuilder RegisterApplication()
        {
            builder = new ContainerBuilder();
            Assembly assembly = Assembly.GetCallingAssembly();
            builder.RegisterAssemblyModules(assembly);
            container = builder.Build();
            return builder;
        }

    }
}
