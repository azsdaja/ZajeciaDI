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
    }
}
