using System;
using Autofac;

namespace ZajeciaDi
{
    public class Program
    {
        static void Main(string[] args)
        {

            // can't create it now like that!
            // var realFacebookService = new FacebookService();

            var containerBuilder = CreateBasicContainerBuilder();
            IContainer container = containerBuilder.Build();

            FacebookService realFacebookService = container.Resolve<FacebookService>();


        }

        public static ContainerBuilder CreateBasicContainerBuilder()
        {
            var containerBuilder = new ContainerBuilder();

            //               twórz taki typ          gdy potrzebny ten interfejs       korzystaj z tej samej instancji
            containerBuilder.RegisterType<Censor>().As<ICensor>().SingleInstance();
            containerBuilder.RegisterType<FacebookService>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<CensorshipApi>().As<ICensorshipApi>().SingleInstance();

            return containerBuilder;
        }
    }
}
