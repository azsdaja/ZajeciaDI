using System;
using Autofac;
using NUnit.Framework;
using Serilog;
using ZajeciaDi;

namespace ZajęciaDI.Tests
{
    public class Notepad
    {
        private class CensorshipApiMock : ICensorshipApi
        {
            public bool IsValid(string text)
            {
                return true;
            }
        }

        [Test]
        public void ManualInjection()
        {
            ILogger loggerMock = null;
            var facebookService = new FacebookService(new Censor(new CensorshipApiMock(), loggerMock));

            var firstPerson = new Person();
            facebookService.Register(firstPerson);

            facebookService.PostContent(firstPerson, "Cześć wam");
            facebookService.PostContent(firstPerson, "Cześć wam głupek");

            Assert.That(firstPerson.Contents.Count, Is.EqualTo(1));
        }

        [Test]
        public void SameTestWithUsingAutofacContainer()
        {
            var containerBuilder = Program.CreateBasicContainerBuilder(); // biorę bazowy builder
            containerBuilder.RegisterType<CensorshipApiMock>().As<ICensorshipApi>(); // nadpisuję rejestrację ICensorshipApi
            
            ILogger loggerMock = null; // tu powinienem sobie stworzyć jakiegoś mocka żeby test przechodził
            containerBuilder.RegisterInstance(loggerMock).As<ILogger>(); // nadpisuję rejestrację ICensorshipApi
            var container = containerBuilder.Build(); // buduję kontener

            var facebookService = container.Resolve<FacebookService>();

            var firstPerson = new Person();
            facebookService.Register(firstPerson);

            facebookService.PostContent(firstPerson, "Cześć wam");
            facebookService.PostContent(firstPerson, "Cześć wam głupek");

            Assert.That(firstPerson.Contents.Count, Is.EqualTo(1));
        }

        [Test]
        public void SzczegolyAutofaca()
        {
            var containerBuilder = new ContainerBuilder();

            //               twórz taki typ          gdy potrzebny ten interfejs       korzystaj z tej samej instancji
            containerBuilder.RegisterType<Censor>().As<ICensor>().SingleInstance();
            // containerBuilder.RegisterType<Censor>().As<ICensor>().InstancePerDependency();

            containerBuilder.RegisterType<FacebookService>().AsSelf().SingleInstance();
            containerBuilder.RegisterType<CensorshipApiAdapter>().As<ICensorshipApi>().SingleInstance();

            ILogger logger = BuildLogger();
            containerBuilder.RegisterInstance(logger).As<ILogger>().SingleInstance();

            var container = containerBuilder.Build(); // buduję kontener

            container.Resolve<ICensor>();
            container.Resolve<ICensor>();
            container.Resolve<ICensor>();
        }

        [Test]
        public void DataGodzina()
        {
            /*var providerMock = Substitute.For<IDateTimeProvider>().For.GetCurrentTime().Returns(new DateTime(2015, 01, 01));

            var testowanyObiektUżywająceBieżacejDaty = new ServiceUsingCurrentTime(providerMock);

            // testuję sobie*/


            var validator = new ShipArrivalDateValidator();

            bool isCorrect = validator.IsCorrect(new DateTime(2020, 6, 7));
            // uwaga - wysypie sie za 3 miesiace ze wzgledu na sztywna zaleznosc od biezacego czasu

            Assert.That(isCorrect, Is.True);
        }

        private ILogger BuildLogger()
        {
            return new LoggerConfiguration()
                // szczegolowa konfiguracja loggera - wymaga dociagniecia pewnych paczek
                /*.Enrich.WithRequestId()
                .Enrich.WithUserName()
                .WriteTo.Console()
                .WriteTo.File($"{AppDomain.CurrentDomain.BaseDirectory}/logs/log_.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://192.168.10.10:9200"))
                    {
                        AutoRegisterTemplate = true,
                    }
                )
                .MinimumLevel.Information() */
                .CreateLogger();
        }

        // Przykładowa funkcja nieprymitywna, a którą można pozostawić jako statyczną - matematyka się nie zmienia
        public static int Power(int baza, int power)
        {
            int wynik = 1;
            for (int i = 0; i < power; i++)
            {
                wynik = wynik * baza;
            }

            return wynik;
        }
    }
}
