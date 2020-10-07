using Autofac;
using AutoMapper;
using Prism.Events;
using System.Collections.Generic;
using TiTacToe.Business.Interfaces;
using TiTacToe.Business.Mappers;
using TiTacToe.Business.ViewModels;
using TiTacToe.Service.Interfaces;
using TiTacToe.Service.Services;

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
            builder.RegisterType<BoardViewModel>().As<IBoardViewModel>();
            builder.RegisterType<ScoreViewModel>().As<IScoreViewModel>();

            //AutoMapper
            RegisterMaps(builder);

            return builder.Build();
        }

        private static void RegisterMaps(ContainerBuilder builder)
        {
            builder.RegisterType<SquareProfile>().As<Profile>();
            builder.Register(c => new MapperConfiguration(cfg =>
            {
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();
        }

    }
}
