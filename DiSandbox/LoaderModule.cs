using Autofac;
using CustomerData.DataAccess;
using DiPresentationLogic;
using System.Windows;

namespace DiSandbox
{
    public class LoaderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>().WithParameter("customerDataFile", "Data/customers.xml").SingleInstance();
            builder.RegisterType<CustomerManagementAgent>().As<ICustomerManagementAgent>();
            builder.RegisterType<MainWindowViewModelFactory>().As<IMainWindowViewModelFactory>();
            builder.RegisterType<MainWindow>().As<Window>();
            builder.RegisterType<MainWindowAdapter>().As<IWindow>();
        }
    }
}
