using Autofac;
using Prism.Events;
using TiTacToe.WPF.Contracts.Services;
using TiTacToe.WPF.Services;
using TiTacToe.WPF.ViewModels;

namespace TiTacToe.WPF.Startup
{
    public class Bootstrapper
    {
        public IContainer BootStrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainViewModel>().AsSelf();

            //Events
            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            //Services
            builder.RegisterType<WindowFactory>().As<IWindowFactory>();
            builder.RegisterType<DialogService>().As<IDialogServices>();

            //viewModels


            return builder.Build();
        }

    }
}
