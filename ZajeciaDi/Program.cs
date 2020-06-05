using System;
using Autofac;

namespace ZajeciaDi
{
    public class Program
    {
        static void Main(string[] args)
        {

            var containerBuilder = CreateBasicContainerBuilder();
            IContainer container = containerBuilder.Build();

            FacebookService realFacebookService = container.Resolve<FacebookService>();
        }

        public static ContainerBuilder CreateBasicContainerBuilder()
        {
            var containerBuilder = new ContainerBuilder();

            //               twórz taki typ          gdy potrzebny ten interfejs       korzystaj z tej samej instancji
            containerBuilder.RegisterType<CensorshipApiAdapter>().As<ICensorshipApi>().SingleInstance();
            containerBuilder.RegisterType<Censor>().As<ICensor>().SingleInstance();
            containerBuilder.RegisterType<FacebookService>().AsSelf().SingleInstance();

            return containerBuilder;
        }
    }
}
