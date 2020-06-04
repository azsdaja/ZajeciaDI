using System;
using Autofac;
using NUnit.Framework;
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
        public void Test()
        {
            var facebookService = new FacebookService(new Censor(new CensorshipApiMock()));

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
            var container = containerBuilder.Build(); // buduję kontener

            var facebookService = container.Resolve<FacebookService>();

            var firstPerson = new Person();
            facebookService.Register(firstPerson);

            facebookService.PostContent(firstPerson, "Cześć wam");
            facebookService.PostContent(firstPerson, "Cześć wam głupek");

            Assert.That(firstPerson.Contents.Count, Is.EqualTo(1));
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
