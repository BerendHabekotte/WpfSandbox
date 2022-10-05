using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using WpfSandbox.Interfaces.ViewModels;
using WpfSandbox.ViewModels;

namespace WpfSandbox
{
    public class LoaderModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>();

        }
    }
}
